using System;
namespace UrlShortener.Shared.Infrastructure.Services
{
    public interface ICodeGenerator
    {
        string GenerateShortCode(int length = 6);
    }
}
