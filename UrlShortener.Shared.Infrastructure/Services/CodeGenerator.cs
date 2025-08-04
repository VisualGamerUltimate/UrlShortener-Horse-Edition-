using System;
using System.Linq;

namespace UrlShortener.Shared.Infrastructure.Services
{
    public class CodeGenerator : ICodeGenerator
    {
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly Random _random = new Random();

        public string GenerateShortCode(int length = 6)
        {
            return new string(Enumerable.Repeat(Characters, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
