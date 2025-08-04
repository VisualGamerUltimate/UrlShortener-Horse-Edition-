using System;

namespace UrlShortener.Shared.Models.Entities
{
    public class ShortenedUrl
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public string OriginalUrl { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int ClickCount { get; set; }
        public bool IsActive { get; set; }
    }
}
