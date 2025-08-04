using System;
namespace UrlShortener.Shared.Models.DTOs
{
    public class CreateUrlRequest
    {
        public string OriginalUrl { get; set; }
        public string? CustomCode { get; set; } 
        public DateTime? ExpiresAt { get; set; } 
    }
}
