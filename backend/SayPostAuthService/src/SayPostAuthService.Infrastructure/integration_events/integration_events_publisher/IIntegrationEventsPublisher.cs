
using SharedKernel.integration_events;

namespace SayPostAuthService.Infrastructure.integration_events.integration_events_publisher;

internal interface IIntegrationEventsPublisher
{
    public Task PublishEvent(IIntegrationEvent integrationEvent);
}
