using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UrlShortener.UrlService.Data;
using UrlShortener.Shared.Models.Entities;

namespace UrlShortener.UrlService.Repositories;
public class UrlRepository : IUrlRepository
{
    private readonly UrlDbContext _context;
    private readonly IDistributedCache _cache;

    public UrlRepository(UrlDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<ShortenedUrl> GetByShortCodeAsync(string shortCode)
    {
        string cacheKey = $"url:{shortCode}";
        var cached = await _cache.GetStringAsync(cacheKey);
        if (cached != null)
            return JsonSerializer.Deserialize<ShortenedUrl>(cached);

        var url = await _context.ShortenedUrls.FirstOrDefaultAsync(
            u => u.ShortCode == shortCode && u.IsActive);

        if (url != null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(url), options);
        }

        return url;
    }

    public async Task<ShortenedUrl> CreateAsync(ShortenedUrl url)
    {
        _context.ShortenedUrls.Add(url);
        await _context.SaveChangesAsync();
        return url;
    }

    public async Task<bool> ShortCodeExistsAsync(string shortCode)
    {
        return await _context.ShortenedUrls.AnyAsync(u => u.ShortCode == shortCode);
    }
}
