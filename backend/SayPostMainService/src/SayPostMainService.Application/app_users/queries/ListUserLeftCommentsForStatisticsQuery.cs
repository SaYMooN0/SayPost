using FollowingsQueryLib.entities;
using FollowingsQueryLib.repository;
using MediatR;
using SayPostMainService.Application.interfaces;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries;

public record class ListUserLeftCommentsForStatisticsQuery(AppUserId UserId)
    : IRequest<ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>>;

public record class PostBriefDataWithComments(
    PublishedPostId PostId,
    AppUserId PostAuthorId,
    PostTitle PostTitle,
    string[] Comments
);

internal class ListUserLeftCommentsForStatisticsQueryHandler :
    IRequestHandler<ListUserLeftCommentsForStatisticsQuery, ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>>
{
    private readonly IUserFollowingsReadRepository _userFollowingsReadRepository;
    private readonly ICurrentActorProvider _currentActorProvider;
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IPostCommentsRepository _postCommentsRepository;
    private readonly IPublishedPostsRepository _publishedPostsRepository;

    public ListUserLeftCommentsForStatisticsQueryHandler(
        IPublishedPostsRepository publishedPostsRepository,
        IAppUsersRepository appUsersRepository,
        IPostCommentsRepository postCommentsRepository,
        IUserFollowingsReadRepository userFollowingsReadRepository,
        ICurrentActorProvider currentActorProvider) {
        _publishedPostsRepository = publishedPostsRepository;
        _appUsersRepository = appUsersRepository;
        _postCommentsRepository = postCommentsRepository;
        _userFollowingsReadRepository = userFollowingsReadRepository;
        _currentActorProvider = currentActorProvider;
    }


    public async Task<ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>> Handle(
        ListUserLeftCommentsForStatisticsQuery query, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdAsNoTracking(query.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found", $"Could not get user's likes. User {query.UserId} was not found"
            );
        }

        if (!user.StatisticsVisibilitySettings.LeftCommentsOnlyForFollowers) {
            return ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>.Success(
                await ExtractPostsWithComments(query.UserId));
        }

        var viewerIdRes = _currentActorProvider.UserId;
        bool isAuthenticated = viewerIdRes.IsSuccess(out var viewerId);
        if (!isAuthenticated) {
            return ErrFactory.NoAccess(
                "Only user's followers can see this information. Please log into your account and follow this user"
            );
        }

        if (viewerId == query.UserId) {
            return ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>.Success(
                await ExtractPostsWithComments(query.UserId));
        }


        if (await IsUserFollowedByAccessor(userId: query.UserId, accessorId: viewerId)) {
            return ErrOr<IReadOnlyCollection<PostBriefDataWithComments>>.Success(
                await ExtractPostsWithComments(query.UserId));
        }

        return ErrFactory.NoAccess(
            "Only user's followers can see this information. Please log into your account and follow this user"
        );
    }

    private async Task<IReadOnlyCollection<PostBriefDataWithComments>> ExtractPostsWithComments(AppUserId userId) {
        var comments = await _postCommentsRepository.GetCommentsByUserAsNoTracking(userId);
        var postIds = comments.Select(p => p.PostId).ToHashSet();
        var posts = await _publishedPostsRepository.GetMultipleAsNoTracking(postIds);
        return posts
            .Select(p => new PostBriefDataWithComments(
                p.Id, p.AuthorId, p.Title,
                comments.Where(c => c.PostId == p.Id).Select(c => c.Content).ToArray()
            ))
            .ToArray();
    }

    private async Task<bool> IsUserFollowedByAccessor(AppUserId userId, AppUserId accessorId) {
        AppUserWithFollowingsData? userWithFollowings = await _userFollowingsReadRepository.GetUser(userId);
        return userWithFollowings!.IsFollowedBy(accessorId);
    }
}