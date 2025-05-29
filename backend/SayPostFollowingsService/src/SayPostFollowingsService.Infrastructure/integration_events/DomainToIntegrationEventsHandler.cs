using InfrastructureShared.integration_events.integration_events_publisher;
using MediatR;
using SayPostFollowingsService.Domain.app_user_aggregate.events;
using SharedKernel.integration_events;

namespace SayPostFollowingsService.Infrastructure.integration_events;

internal class DomainToIntegrationEventsHandler :
    INotificationHandler<UserGotNewFollowingEvent>
// and all other domain events that need to be published as integration events
{
    private readonly IIntegrationEventsPublisher _integrationEventsPublisher;

    public DomainToIntegrationEventsHandler(IIntegrationEventsPublisher integrationEventsPublisher) {
        _integrationEventsPublisher = integrationEventsPublisher;
    }

    public async Task Handle(UserGotNewFollowingEvent notification, CancellationToken cancellationToken) {
        var integrationEvent = new UserGotNewFollowerIntegrationEvent(
            UserId: notification.FollowingId,
            FollowerId: notification.UserThatFollowedId
        );
        await _integrationEventsPublisher.PublishEvent(integrationEvent);
    }
}