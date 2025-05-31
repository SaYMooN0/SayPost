using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Application.app_users.queries;

public record class ListUserPublishedPostsQuery(AppUserId UserId)
    : IRequest<IReadOnlyCollection<PublishedPost>>;

internal class ListUserPublishedPostsQueryHandler :
    IRequestHandler<ListUserPublishedPostsQuery, IReadOnlyCollection<PublishedPost>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;

    public ListUserPublishedPostsQueryHandler(IPublishedPostsRepository publishedPostsRepository) {
        _publishedPostsRepository = publishedPostsRepository;
    }


    public async Task<IReadOnlyCollection<PublishedPost>> Handle(
        ListUserPublishedPostsQuery query, CancellationToken cancellationToken
    ) {
        return await _publishedPostsRepository.ListPostsByUserAsNoTracking(query.UserId);
    }
}