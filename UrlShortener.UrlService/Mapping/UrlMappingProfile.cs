using AutoMapper;
using UrlShortener.Shared.Models.Entities;
using UrlShortener.Shared.Models.DTOs;
namespace UrlShortener.UrlService.Mapping;

public class UrlMappingProfile : Profile
{
    public UrlMappingProfile()
    {
        CreateMap<ShortenedUrl, UrlDto>()
        .ForMember(dest => dest.ShortUrl, opt =>
        opt.MapFrom(src => $"https://yourdomain.com/{src.ShortCode}"));
    }
}

