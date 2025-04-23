using InfrastructureShared.integration_events.integration_events_publisher;
using MediatR;
using SayPostMainService.Domain.published_post_aggregate.events;
using SharedKernel.integration_events;

namespace SayPostMainService.Infrastructure.integration_events;

internal class DomainToIntegrationEventsHandler :
    INotificationHandler<NewPublishedPostCreated>
// and all other domain events that need to be published as integration events
{
    private readonly IIntegrationEventsPublisher _integrationEventsPublisher;

    public DomainToIntegrationEventsHandler(IIntegrationEventsPublisher integrationEventsPublisher) {
        _integrationEventsPublisher = integrationEventsPublisher;
    }

    public async Task Handle(NewPublishedPostCreated notification, CancellationToken cancellationToken) {
        var integrationEvent = new NewPostPublishedIntegrationEvent(notification.PostId, notification.AuthorId);
        await _integrationEventsPublisher.PublishEvent(integrationEvent);
    }
}