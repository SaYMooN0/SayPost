using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.post_tag_aggregate;
using SayPostMainService.Domain.published_post_aggregate.events;

namespace SayPostMainService.Application.post_tags.event_handlers;

public class NewPublishedPostCreatedEventHandler : INotificationHandler<NewPublishedPostCreatedEvent>
{
    private readonly IPostTagsRepository _postTagsRepository;
    public NewPublishedPostCreatedEventHandler(IPostTagsRepository postTagsRepository) {
        _postTagsRepository = postTagsRepository;
    }

    public async Task Handle(NewPublishedPostCreatedEvent notification, CancellationToken cancellationToken) {

        var existingTags = await _postTagsRepository.GetTagsWithIds( notification.PostTags);
        foreach (var tag in existingTags) {
            tag.IncrementPostWithThisTagCount();
        }
        if (existingTags.Length > 0) {
            await _postTagsRepository.UpdateRange(existingTags);
        }
        
        
        var existingTagIds = existingTags.Select(t => t.Id).ToHashSet();
        var newTagIds = notification.PostTags.ToHashSet().Except(existingTagIds).ToArray();

        var newTags = newTagIds
            .Select(id => PostTag.Create(id, 1))
            .ToArray();
        
        if (newTags.Length > 0) {
            await _postTagsRepository.AddRange(newTags);
        }
            
    }
}