using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.post_comment_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.comments.queries;

public record class CommentsForPostQuery(
    PublishedPostId PostId,
    string SortOption
) : IRequest<ErrOr<IReadOnlyCollection<PostComment>>>;

internal class CommentsForPostQueryHandler :
    IRequestHandler<CommentsForPostQuery, ErrOr<IReadOnlyCollection<PostComment>>>
{
    private readonly IPostCommentsRepository _postCommentsRepository;

    public CommentsForPostQueryHandler(IPostCommentsRepository postCommentsRepository) {
        this._postCommentsRepository = postCommentsRepository;
    }

    public async Task<ErrOr<IReadOnlyCollection<PostComment>>> Handle(
        CommentsForPostQuery query, CancellationToken cancellationToken
    ) {
        CommentsSortOption sortOption;
        try {
            sortOption = Enum.Parse<CommentsSortOption>(query.SortOption, ignoreCase: true);
        }
        catch {
            return ErrFactory.InvalidData($"Incorrect sort option provided: {query.SortOption}");
        }

        var comments = await _postCommentsRepository
            .GetCommentsForPostAsNoTracking(query.PostId, sortOption);
        return ErrOr<IReadOnlyCollection<PostComment>>.Success(comments);
    }
}