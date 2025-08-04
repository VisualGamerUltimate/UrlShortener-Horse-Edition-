using UrlShortener.Shared.Models.Entities;
namespace UrlShortener.UrlService.Repositories;
public interface IUrlRepository
{
    Task<ShortenedUrl> GetByShortCodeAsync(string shortCode);
    Task<ShortenedUrl> CreateAsync(ShortenedUrl url);
    Task<bool> ShortCodeExistsAsync(string shortCode);
}
