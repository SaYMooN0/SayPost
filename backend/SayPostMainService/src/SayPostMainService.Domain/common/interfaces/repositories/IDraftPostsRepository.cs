using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IDraftPostsRepository
{
    Task Add(DraftPost draftPost);
    Task Update(DraftPost draftPost);
}