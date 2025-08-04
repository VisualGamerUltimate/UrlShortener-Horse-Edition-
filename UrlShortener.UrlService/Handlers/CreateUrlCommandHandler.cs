using AutoMapper;
using MediatR;
using UrlShortener.Shared.Models.DTOs;
using UrlShortener.Shared.Models.Entities;
using UrlShortener.Shared.Infrastructure.Services;
using UrlShortener.UrlService.Commands;
using UrlShortener.UrlService.Repositories;

namespace UrlShortener.UrlService.Handlers;

public class CreateUrlCommandHandler : IRequestHandler<CreateUrlCommand, UrlDto>
{
    private readonly IUrlRepository _repo;
    private readonly ICodeGenerator _codeGen;
    private readonly IUrlValidator _validator;
    private readonly IMapper _mapper;

    public CreateUrlCommandHandler(IUrlRepository repo, ICodeGenerator codeGen, IUrlValidator validator, IMapper mapper)
    {
        _repo = repo;
        _codeGen = codeGen;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<UrlDto> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
    {
        if (!_validator.IsValidUrl(request.OriginalUrl))
            throw new ArgumentException("Invalid URL");

        string code = request.CustomCode;
        if (string.IsNullOrEmpty(code))
        {
            do
            {
                code = _codeGen.GenerateShortCode();
            } while (await _repo.ShortCodeExistsAsync(code));
        }
        else if (await _repo.ShortCodeExistsAsync(code))
        {
            throw new ArgumentException("Custom short code already exists");
        }

        var url = new ShortenedUrl
        {
            OriginalUrl = request.OriginalUrl,
            ShortCode = code,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = request.ExpiresAt,
            ClickCount = 0,
            IsActive = true,
            UserId = request.UserId
        };

        var created = await _repo.CreateAsync(url);
        return _mapper.Map<UrlDto>(created);
    }
}
