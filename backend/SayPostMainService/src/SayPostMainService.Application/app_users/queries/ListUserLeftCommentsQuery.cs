using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries;

public record class ListUserLeftCommentsQuery(AppUserId UserId)
    : IRequest<ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>>;

public record class PostBriefDataWithComments(
    PublishedPostId PostId,
    AppUserId PostAuthorId,
    PostTitle PostTitle,
    string[] Comments
);

internal class ListUserLeftCommentsQueryHandler :
    IRequestHandler<ListUserLeftCommentsQuery, ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IPostCommentsRepository _postCommentsRepository;
    private readonly IPublishedPostsRepository _publishedPostsRepository;

    public ListUserLeftCommentsQueryHandler(
        IPublishedPostsRepository publishedPostsRepository,
        IAppUsersRepository appUsersRepository,
        IPostCommentsRepository postCommentsRepository
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _appUsersRepository = appUsersRepository;
        _postCommentsRepository = postCommentsRepository;
    }


    public async Task<ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>> Handle(
        ListUserLeftCommentsQuery query, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdAsNoTracking(query.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found", $"Could not get user's likes. User {query.UserId} was not found"
            );
        }

        var comments = await _postCommentsRepository.GetCommentsByUserAsNoTracking(query.UserId);
        var postIds = comments.Select(p => p.PostId).ToHashSet();
        var posts = await _publishedPostsRepository.GetMultipleAsNoTracking(postIds);
        IReadOnlyCollection<PostBriefDataWithComments> postsWithComments = posts
            .Select(p => new PostBriefDataWithComments(
                p.Id, p.AuthorId, p.Title,
                comments.Where(c => c.PostId == p.Id).Select(c => c.Content).ToArray()
            ))
            .ToArray();
        return ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>.Success(postsWithComments);
    }
}