using InfrastructureShared.integration_events.integration_events_publisher;
using MediatR;
using SayPostMainService.Domain.post_comment_aggregate.events;
using SayPostMainService.Domain.published_post_aggregate.events;
using SharedKernel.integration_events;

namespace SayPostMainService.Infrastructure.integration_events;

internal class DomainToIntegrationEventsHandler :
    INotificationHandler<NewPublishedPostCreatedEvent>,
    INotificationHandler<NewCommentToPostAddedEvent>
// and all other domain events that need to be published as integration events
{
    private readonly IIntegrationEventsPublisher _integrationEventsPublisher;

    public DomainToIntegrationEventsHandler(IIntegrationEventsPublisher integrationEventsPublisher) {
        _integrationEventsPublisher = integrationEventsPublisher;
    }

    public async Task Handle(NewPublishedPostCreatedEvent notification, CancellationToken cancellationToken) {
        var integrationEvent = new NewPostPublishedIntegrationEvent(notification.PostId, notification.AuthorId);
        await _integrationEventsPublisher.PublishEvent(integrationEvent);
    }

    public async Task Handle(NewCommentToPostAddedEvent notification, CancellationToken cancellationToken) {
        var integrationEvent = new NewCommentUnderPostLeftIntegrationEvent(
            notification.PostAuthorId,
            notification.PostTitle.ToString(),
            notification.CommentAuthorId
        );
        await _integrationEventsPublisher.PublishEvent(integrationEvent);
    }
}