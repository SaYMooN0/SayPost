using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SharedKernel.Configs;
using SharedKernel.integration_events;

namespace SayPostAuthService.Infrastructure.integration_events.integration_events_publisher;

internal class IntegrationEventsPublisher : IIntegrationEventsPublisher
{
    private readonly MessageBrokerConfig _brokerConfig;
    private readonly ILogger<IntegrationEventsPublisher> _logger;

    public IntegrationEventsPublisher(IOptions<MessageBrokerConfig> messageBrokerOptions) {
        _brokerConfig = messageBrokerOptions.Value;
    }

    public async Task PublishEvent(IIntegrationEvent integrationEvent) {
        try {
            var connectionFactory = new ConnectionFactory {
                HostName = _brokerConfig.HostName,
                Port = _brokerConfig.Port,
                UserName = _brokerConfig.UserName,
                Password = _brokerConfig.Password
            };

            using var connection = await connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            var exchangeName = _brokerConfig.ExchangeName;

            await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Fanout, durable: true);
            await channel.QueueDeclareAsync(queue: _brokerConfig.QueueName, durable: false, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(
                queue: _brokerConfig.QueueName,
                exchange: exchangeName,
                routingKey: string.Empty
            );
            var message = JsonSerializer.Serialize(integrationEvent);
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: exchangeName, routingKey: string.Empty, body: body);


            _logger.LogInformation($"Event of type {integrationEvent.GetType().Name} published to exchange {exchangeName}.");
        } catch (Exception ex) {
            _logger.LogError($"Error occurred while publishing event: {ex.Message}");
            throw;
        }
    }
}
