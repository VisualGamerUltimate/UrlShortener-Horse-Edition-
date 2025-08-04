using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Shared.EventBus;
using UrlShortener.Shared.EventBus.Events;
using UrlShortener.Shared.Models.DTOs;
using UrlShortener.UrlService.Commands;
using UrlShortener.UrlService.Queries;

namespace UrlShortener.UrlService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEventBus _eventBus;


        public UrlsController(IMediator mediator, IEventBus eventBus)
        {
            _mediator = mediator;
            _eventBus = eventBus;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUrlRequest request)
        {
            var command = new CreateUrlCommand(
                request.OriginalUrl,
                UserId: "user123", // You can replace this with real user ID from auth later
                request.CustomCode,
                request.ExpiresAt
            );

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectTo(string shortCode)
        {
            try
            {
                var url = await _mediator.Send(new GetUrlByShortCodeQuery(shortCode));
                if (url == null)
                    return NotFound();

                // Publish event to RabbitMQ
                var clickEvent = new UrlClickedEvent
                {
                    UrlId = url.Id,
                    ShortCode = url.ShortCode,
                    UserId = "user123", // Replace with actual user if needed
                    ClickedAt = DateTime.UtcNow,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
                };

                await _eventBus.PublishAsync(clickEvent);

                return Redirect(url.OriginalUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine("RedirectTo ERROR: " + ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}