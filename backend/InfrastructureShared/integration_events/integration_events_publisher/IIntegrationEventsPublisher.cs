
using SharedKernel.integration_events;

namespace InfrastructureShared.integration_events.integration_events_publisher;

public interface IIntegrationEventsPublisher
{
    public Task PublishEvent(IIntegrationEvent integrationEvent);
}
