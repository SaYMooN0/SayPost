using SayPostMainService.Domain.common;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.draft_post_aggregate.events;

public record class NewDraftPostCreatedEvent(DraftPostId DraftPostId, AppUserId AuthorId): IDomainEvent;