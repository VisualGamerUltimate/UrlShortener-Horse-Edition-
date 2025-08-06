using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using UrlShortener.Shared.EventBus;
using UrlShortener.Shared.Infrastructure.Services;
using UrlShortener.Shared.Models.DTOs;
using UrlShortener.Shared.Models.Entities;
using UrlShortener.UrlService.Data;
using UrlShortener.UrlService.Mapping;
using UrlShortener.UrlService.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
options.ListenAnyIP(80);
});

var environment = builder.Environment;

// 1. Database configuration
if (environment.IsEnvironment("Testing"))
{
builder.Services.AddDbContext<UrlDbContext>(options =>
options.UseInMemoryDatabase("TestUrlDb"));
// Fallback IDistributedCache using memory
builder.Services.AddSingleton<IDistributedCache, MemoryDistributedCache>();
builder.Services.AddSingleton<IOptions<MemoryDistributedCacheOptions>, OptionsWrapper<MemoryDistributedCacheOptions>>(
    _ => new OptionsWrapper<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions()));

builder.Services.AddSingleton<IEventBus, NoOpEventBus>();

}
else
{
builder.Services.AddDbContext<UrlDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection"),
sql => sql.EnableRetryOnFailure()));
}

// 2. Redis (skip during testing)
if (!environment.IsEnvironment("Testing"))
{
builder.Services.AddStackExchangeRedisCache(options =>
{
options.Configuration = "redis:6379";
});
}

// 3. AutoMapper
builder.Services.AddAutoMapper(typeof(UrlMappingProfile));

// 4. Services
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<ICodeGenerator, CodeGenerator>();
builder.Services.AddScoped<IUrlValidator, UrlValidator>();
if (environment.IsEnvironment("Testing"))
{
    builder.Services.AddSingleton<IEventBus, NoOpEventBus>();
}
else
{
    builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();
}

// 5. MediatR
builder.Services.AddMediatR(cfg =>
{
cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// 6. Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (!environment.IsEnvironment("Testing"))
{
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<UrlDbContext>();
db.Database.Migrate(); // only run migrations outside of testing
}

if (environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

if (!environment.IsEnvironment("Docker"))
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

namespace UrlShortener.UrlService
{
    public partial class Program { }
}

