using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedKernel.configs;
using SharedKernel.integration_events;

namespace InfrastructureShared.integration_events.background_service;

public class ConsumeIntegrationEventsBackgroundService : IHostedService
{
    private readonly ILogger<ConsumeIntegrationEventsBackgroundService> _logger;
    private readonly MessageBrokerConfig _messageBrokerConfig;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private IConnection _connection;
    private IChannel _channel;

    public ConsumeIntegrationEventsBackgroundService(
        ILogger<ConsumeIntegrationEventsBackgroundService> logger,
        IOptions<MessageBrokerConfig> messageBrokerOptions,
        IServiceScopeFactory serviceScopeFactory
    ) {
        _logger = logger;
        _messageBrokerConfig = messageBrokerOptions.Value;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("Starting integration event consumer background service.");

        var connectionFactory = new ConnectionFactory {
            HostName = _messageBrokerConfig.HostName,
            Port = _messageBrokerConfig.Port,
            UserName = _messageBrokerConfig.UserName,
            Password = _messageBrokerConfig.Password
        };
        _connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
        await SetupMessageBrokerAsync(cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (sender, eventArgs) =>
            await HandleEventAsync(sender, eventArgs, cancellationToken);

        await _channel.BasicConsumeAsync(
            _messageBrokerConfig.QueueName
            , autoAck: false,
            consumer: consumer,
            cancellationToken: cancellationToken
        );
    }

    private async Task SetupMessageBrokerAsync(CancellationToken cancellationToken) {
        await _channel.ExchangeDeclareAsync(
            _messageBrokerConfig.ExchangeName,
            ExchangeType.Fanout,
            durable: true,
            cancellationToken: cancellationToken
        );

        var queueDeclareResult = await _channel.QueueDeclareAsync(
            queue: _messageBrokerConfig.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken
        );

        await _channel.QueueBindAsync(
            queue: _messageBrokerConfig.QueueName,
            exchange: _messageBrokerConfig.ExchangeName,
            routingKey: string.Empty,
            cancellationToken: cancellationToken
        );
    }

    private async Task HandleEventAsync(
        object sender, BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken
    ) {
        if (cancellationToken.IsCancellationRequested) {
            _logger.LogInformation("Cancellation requested, not consuming integration event");
            return;
        }

        try {
            _logger.LogInformation("Received integration event. Reading message from queue");

            using var scope = _serviceScopeFactory.CreateScope();

            byte[] body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(message);
            if (integrationEvent is null) {
                throw new Exception("Integration event is null.");
            }

            _logger.LogInformation(
                "Received integration event of type: {IntegrationEventType}. Publishing event.",
                integrationEvent.GetType().Name
            );

            var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
            await publisher.Publish(integrationEvent, cancellationToken);

            _logger.LogInformation("Integration event published successfully. Sending ack to message broker.");
            await _channel.BasicAckAsync(eventArgs.DeliveryTag, multiple: false, cancellationToken);
        }
        catch (Exception e) {
            _logger.LogError(e, "Exception occurred while consuming integration event.");
            await _channel.BasicNackAsync(eventArgs.DeliveryTag, false, true, cancellationToken);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken) {
        try {
            if (_channel is not null && _connection is not null) {
                await _channel.CloseAsync(cancellationToken);
                _channel.Dispose();

                await _connection.CloseAsync(cancellationToken);
                _connection.Dispose();
            }
        }
        catch (Exception e) {
            _logger.LogError(e, "Error while stopping the consumer service.");
        }
    }
}