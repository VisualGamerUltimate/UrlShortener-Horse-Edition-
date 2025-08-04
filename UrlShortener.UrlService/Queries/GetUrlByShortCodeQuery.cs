using MediatR;
using UrlShortener.Shared.Models.DTOs;

namespace UrlShortener.UrlService.Queries
{
    public record GetUrlByShortCodeQuery(string ShortCode) : IRequest<UrlDto>;
}



