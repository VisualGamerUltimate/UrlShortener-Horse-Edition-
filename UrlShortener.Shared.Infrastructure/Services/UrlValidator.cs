using System;

namespace UrlShortener.Shared.Infrastructure.Services
{
    public class UrlValidator : IUrlValidator
    {
        public bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
