using AutoMapper;
using MediatR;
using UrlShortener.Shared.Models.DTOs;
using UrlShortener.Shared.Models.Entities;
using UrlShortener.UrlService.Queries;
using UrlShortener.UrlService.Repositories;

namespace UrlShortener.UrlService.Handlers;

public class GetUrlByShortCodeHandler : IRequestHandler<GetUrlByShortCodeQuery, UrlDto>
{
    private readonly IUrlRepository _repo;
    private readonly IMapper _mapper;

    public GetUrlByShortCodeHandler(IUrlRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<UrlDto> Handle(GetUrlByShortCodeQuery request, CancellationToken cancellationToken)
    {
        var url = await _repo.GetByShortCodeAsync(request.ShortCode);
        return url == null ? null : _mapper.Map<UrlDto>(url);
    }
}
