using InfrastructureShared.integration_events.integration_events_publisher;

namespace SayPostAuthService.Infrastructure.integration_events;

internal class DomainToIntegrationEventsHandler 
// and all other domain events that need to be published as integration events
{
    private readonly IIntegrationEventsPublisher _integrationEventsPublisher;

    public DomainToIntegrationEventsHandler(IIntegrationEventsPublisher integrationEventsPublisher) {
        _integrationEventsPublisher = integrationEventsPublisher;
    }
}