using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.UrlService;
using UrlShortener.UrlService.Data;
using UrlShortener.UserService.Services;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing"); // Will skip UseHttpsRedirection, etc.

        builder.ConfigureServices(services =>
    {
        // Remove the production DB context
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<UrlDbContext>));
        if (descriptor != null)
            services.Remove(descriptor);

        // Register in-memory test DB
        services.AddDbContext<UrlDbContext>(options =>
            options.UseInMemoryDatabase("TestUrlDb"));

        services.AddSingleton<JwtService>();

        // Apply migrations/data seeding if needed
        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<UrlDbContext>();
        db.Database.EnsureCreated();
    });
    }
}