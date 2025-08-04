using System;
namespace UrlShortener.Shared.Infrastructure.Services
{
    public interface IUrlValidator
    {
        bool IsValidUrl(string url);
    }
}
