namespace UrlShortener.Shared.EventBus
{
    public class NoOpEventBus : IEventBus
    {
        public Task PublishAsync<T>(T @event) where T : class
        {
            // Do nothing and complete the task
            return Task.CompletedTask;
        }


        public Task SubscribeAsync<T>(Func<T, Task> handler) where T : class
        {
            // Do nothing and complete the task
            return Task.CompletedTask;
        }
    }
}