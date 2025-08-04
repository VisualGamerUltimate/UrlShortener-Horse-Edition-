using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;
using UrlShortener.UserService;
using UrlShortener.UserService.Data;
using UrlShortener.UserService.Services;

public class CustomUserServiceFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Testing");


        builder.ConfigureServices(services =>
    {
        // Remove the existing DbContext registration
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<UserDbContext>));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        // Register InMemory DB
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseInMemoryDatabase("TestUserDb");
        });
        // Register JwtService
        services.AddSingleton<JwtService>();

        // Build service provider and initialize DB
        var sp = services.BuildServiceProvider();

        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        db.Database.EnsureCreated();
    });

        return base.CreateHost(builder);
    }
}

public class UserServiceIntegrationTests : IClassFixture<CustomUserServiceFactory>
{
    private readonly HttpClient _client;


public UserServiceIntegrationTests(CustomUserServiceFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_Returns_Success()
    {
        var request = new
        {
            username = $"user_{Guid.NewGuid():N}",
            email = $"integration_{Guid.NewGuid():N}@example.com",
            password = "securepass"
        };

        var json = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/auth/register", json);
        var body = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, $"Status: {response.StatusCode}\nBody: {body}");
    }
}