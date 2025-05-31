using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.queries;

public record class ListUserLikedPostsQuery(AppUserId UserId)
    : IRequest<ErrOr<IReadOnlyCollection<PublishedPost>>>;

internal class ListUserLikedPostsQueryHandler :
    IRequestHandler<ListUserLikedPostsQuery, ErrOr<IReadOnlyCollection<PublishedPost>>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IPublishedPostsRepository _publishedPostsRepository;

    public ListUserLikedPostsQueryHandler(
        IPublishedPostsRepository publishedPostsRepository, IAppUsersRepository appUsersRepository
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _appUsersRepository = appUsersRepository;
    }


    public async Task<ErrOr<IReadOnlyCollection<PublishedPost>>> Handle(
        ListUserLikedPostsQuery query, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdAsNoTracking(query.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found", $"Could not get user's likes. User {query.UserId} was not found"
            );
        }

        var posts = await _publishedPostsRepository.GetMultipleAsNoTracking(user.LikedPostIds);
        return ErrOr<IReadOnlyCollection<PublishedPost>>.Success(posts);
    }
}