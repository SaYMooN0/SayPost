using SayPostMainService.Domain.post_comment_aggregate;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IPostCommentsRepository
{
    Task<IReadOnlyCollection<PostComment>> GetCommentsForPostAsNoTracking(
        PublishedPostId postId, CommentsSortOption sortOption
    );

    Task Add(PostComment comment);
    Task<IReadOnlyCollection<PostComment>> GetCommentsByUserAsNoTracking(AppUserId userId);
}