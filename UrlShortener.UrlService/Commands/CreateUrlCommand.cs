using MediatR;
using UrlShortener.Shared.Models.DTOs;

namespace UrlShortener.UrlService.Commands
{
    public record CreateUrlCommand(
    string OriginalUrl,
    string UserId,
    string? CustomCode = null,
    DateTime? ExpiresAt = null)
    : IRequest<UrlDto>;
}

