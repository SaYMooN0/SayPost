using FollowingsQueryLib.entities;
using FollowingsQueryLib.repository;
using MediatR;
using SayPostMainService.Application.interfaces;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries.specific_statistics_fields;

public record class ListUserPublishedPostsForStatisticsQuery(AppUserId UserId)
    : IRequest<ErrOr<IReadOnlyCollection<PublishedPost>>>;

internal class ListUserPublishedPostsForStatisticsQueryHandler :
    IRequestHandler<ListUserPublishedPostsForStatisticsQuery, ErrOr<IReadOnlyCollection<PublishedPost>>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly ICurrentActorProvider _currentActorProvider;
    private readonly IUserFollowingsReadRepository _userFollowingsReadRepository;

    public ListUserPublishedPostsForStatisticsQueryHandler(
        IPublishedPostsRepository publishedPostsRepository,
        IAppUsersRepository appUsersRepository,
        ICurrentActorProvider currentActorProvider,
        IUserFollowingsReadRepository userFollowingsReadRepository
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _appUsersRepository = appUsersRepository;
        _currentActorProvider = currentActorProvider;
        _userFollowingsReadRepository = userFollowingsReadRepository;
    }

    public async Task<ErrOr<IReadOnlyCollection<PublishedPost>>> Handle(
        ListUserPublishedPostsForStatisticsQuery query, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdAsNoTracking(query.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found", $"Could not get user's published posts. User {query.UserId} was not found"
            );
        }

        if (!user.StatisticsVisibilitySettings.PublishedPostsOnlyForFollowers) {
            return await ExtractPublishedPosts(query.UserId);
        }

        var viewerIdRes = _currentActorProvider.UserId;
        bool isAuthenticated = viewerIdRes.IsSuccess(out var viewerId);

        if (!isAuthenticated) {
            return ErrFactory.NoAccess(
                "Only user's followers can see this information. Please log into your account and follow this user"
            );
        }

        if (viewerId == query.UserId) {
            return await ExtractPublishedPosts(query.UserId);
        }

        if (await IsUserFollowedByAccessor(query.UserId, viewerId)) {
            return await ExtractPublishedPosts(query.UserId);
        }

        return ErrFactory.NoAccess(
            "Only user's followers can see this information. Please log into your account and follow this user"
        );
    }

    private async Task<ErrOr<IReadOnlyCollection<PublishedPost>>> ExtractPublishedPosts(AppUserId userId)
    {
        var posts = await _publishedPostsRepository.ListPostsByUserAsNoTracking(userId);
        return ErrOr<IReadOnlyCollection<PublishedPost>>.Success(posts);
    }

    private async Task<bool> IsUserFollowedByAccessor(AppUserId userId, AppUserId accessorId)
    {
        AppUserWithFollowingsData? userWithFollowings = await _userFollowingsReadRepository.GetUser(userId);
        return userWithFollowings?.IsFollowedBy(accessorId) == true;
    }
}
