using System;
using System.Threading.Tasks;

namespace UrlShortener.Shared.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : class;
        Task SubscribeAsync<T>(Func<T, Task> handler) where T : class;
    }
}
