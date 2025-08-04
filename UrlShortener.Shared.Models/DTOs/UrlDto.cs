using System;
namespace UrlShortener.Shared.Models.DTOs
{
    public class UrlDto
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ClickCount { get; set; }
    }
}
