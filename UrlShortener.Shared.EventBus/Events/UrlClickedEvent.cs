using System;
namespace UrlShortener.Shared.EventBus.Events
{
    public class UrlClickedEvent
    {
        public int UrlId { get; set; }
        public string ShortCode { get; set; }
        public string UserId { get; set; }
        public DateTime ClickedAt { get; set; }
        public string IpAddress { get; set; }
    }
}
