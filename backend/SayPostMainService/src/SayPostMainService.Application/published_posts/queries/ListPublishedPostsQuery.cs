using System.Collections.Immutable;
using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.published_posts.queries;

public record class ListPublishedPostsQuery(DateTime? DateFrom, DateTime? DateTo, string[] IncludeTags) :
    IRequest<ErrOr<ImmutableArray<PublishedPost>>>;

internal class ListPublishedPostsQueryHandler
    : IRequestHandler<ListPublishedPostsQuery, ErrOr<ImmutableArray<PublishedPost>>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ListPublishedPostsQueryHandler(
        IPublishedPostsRepository publishedPostsRepository,
        IDateTimeProvider dateTimeProvider
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrOr<ImmutableArray<PublishedPost>>> Handle(
        ListPublishedPostsQuery request, CancellationToken cancellationToken
    ) {
        var filer = CreateFilter(request, _dateTimeProvider);
        if (filer.IsErr(out var err)) {
            return err;
        }

        var tags = await _publishedPostsRepository.QueryPosts(filer.AsSuccess());

        return tags.ToImmutableArray();
    }

    private static ErrOr<PostsQueryFilter> CreateFilter(ListPublishedPostsQuery r, IDateTimeProvider provider) {
        return new PostsQueryFilter(null, null, []);
    }
}