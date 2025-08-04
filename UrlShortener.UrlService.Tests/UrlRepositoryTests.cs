using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using UrlShortener.Shared.Models.Entities;
using UrlShortener.UrlService.Data;
using UrlShortener.UrlService.Repositories;
using Xunit;

public class UrlRepositoryTests
{
    private readonly UrlRepository _repo;


public UrlRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<UrlDbContext>()
            .UseInMemoryDatabase("UrlDbTest")
            .Options;

        var db = new UrlDbContext(options);
        var cache = new Mock<IDistributedCache>();

        _repo = new UrlRepository(db, cache.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_PersistUrl()
    {
        var url = new ShortenedUrl
        {
            ShortCode = "abc123",
            OriginalUrl = "https://test.com",
            CreatedAt = System.DateTime.UtcNow,
            IsActive = true,
            UserId = "test-user-id"
        };

        var result = await _repo.CreateAsync(url);

        Assert.Equal("abc123", result.ShortCode);
    }
}