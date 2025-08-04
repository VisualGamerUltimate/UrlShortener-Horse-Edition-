using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using UrlShortener.Shared.EventBus;

public class RabbitMqEventBus : IEventBus
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMqEventBus> _logger;

    public RabbitMqEventBus(IConfiguration config, ILogger<RabbitMqEventBus> logger)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        _logger = logger;
        if (environment == "Testing")
        {
            _connection = null;
            _logger = logger;
            return;
        }

        var factory = new ConnectionFactory()
        {
            HostName = config["RabbitMQ:HostName"] ?? "rabbitmq",
            UserName = config["RabbitMQ:UserName"] ?? "guest",
            Password = config["RabbitMQ:Password"] ?? "guest"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public async Task PublishAsync<T>(T @event) where T : class
    {
        var eventName = typeof(T).Name;
        _channel.QueueDeclare(eventName, durable: true, exclusive: false, autoDelete: false);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
        _channel.BasicPublish("", eventName, null, body);

        _logger.LogInformation($"[RabbitMQ] Published event: {eventName}");
        await Task.CompletedTask;
    }

    public async Task SubscribeAsync<T>(Func<T, Task> handler) where T : class
    {
        // We'll implement this in UserService later
        await Task.CompletedTask;
    }
}
