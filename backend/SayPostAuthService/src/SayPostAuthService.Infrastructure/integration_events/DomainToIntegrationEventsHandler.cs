using InfrastructureShared.integration_events.integration_events_publisher;
using MediatR;
using SayPostAuthService.Domain.app_user_aggregate.events;
using SharedKernel.integration_events;

namespace SayPostAuthService.Infrastructure.integration_events;

internal class DomainToIntegrationEventsHandler  :
    INotificationHandler<NewAppUserCreatedEvent>
// and all other domain events that need to be published as integration events
{
    private readonly IIntegrationEventsPublisher _integrationEventsPublisher;

    public DomainToIntegrationEventsHandler(IIntegrationEventsPublisher integrationEventsPublisher) {
        _integrationEventsPublisher = integrationEventsPublisher;
    }

    public async Task Handle(NewAppUserCreatedEvent notification, CancellationToken cancellationToken) {
        var integrationEvent = new NewAppUserCreatedIntegrationEvent(notification.CreatedUserId);
        await _integrationEventsPublisher.PublishEvent(integrationEvent);    
    }
}