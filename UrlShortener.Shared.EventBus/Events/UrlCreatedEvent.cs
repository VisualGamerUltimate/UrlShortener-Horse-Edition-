using System;
namespace UrlShortener.Shared.EventBus.Events
{
    public class UrlCreatedEvent
    {
        public int UrlId { get; set; }
        public string ShortCode { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
