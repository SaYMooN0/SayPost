using System.Collections.Immutable;
using MediatR;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.published_posts.queries;

public record class ListPostsWithCommentsQuery(
    long? DateFrom,
    long? DateTo,
    string? IncludeTags,
    string? ExcludeTags
) : IRequest<ErrOr<ImmutableArray<PublishedPost>>>;

internal class ListPostsWithCommentsQueryHandler
    : IRequestHandler<ListPostsWithCommentsQuery, ErrOr<ImmutableArray<PublishedPost>>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ListPostsWithCommentsQueryHandler(
        IPublishedPostsRepository publishedPostsRepository,
        IDateTimeProvider dateTimeProvider
    ) {
        _publishedPostsRepository = publishedPostsRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrOr<ImmutableArray<PublishedPost>>> Handle(
        ListPostsWithCommentsQuery request, CancellationToken cancellationToken
    ) {
        var filer = CreateFilter(request, _dateTimeProvider);
        if (filer.IsErr(out var err)) {
            return err;
        }

        var posts = await _publishedPostsRepository.ListPostsWithFilterAsNoTracking(filer.AsSuccess());

        return posts.ToImmutableArray();
    }

    private static ErrOr<PostsQueryFilter> CreateFilter(
        ListPostsWithCommentsQuery r,
        IDateTimeProvider dateTimeProvider
    ) {
        DateTime? dateFrom = r.DateFrom.HasValue
            ? DateTimeOffset
                .FromUnixTimeMilliseconds(r.DateFrom.Value)
                .UtcDateTime
                .Date
            : null;

        DateTime? dateTo = r.DateTo.HasValue
            ? DateTimeOffset
                .FromUnixTimeMilliseconds(r.DateTo.Value)
                .UtcDateTime
                .Date
                .AddDays(1)
                .AddTicks(-1)
            : null;

        if (dateFrom.HasValue && dateFrom > dateTimeProvider.Now.AddDays(1)) {
            return ErrFactory.InvalidData("Date 'From' cannot be greater than the current date");
        }

        if (dateFrom.HasValue && dateTo.HasValue && dateFrom >= dateTo) {
            return ErrFactory.InvalidData("Date 'From' must be less than date 'To'");
        }

        var includeTags = ParseTags(r.IncludeTags);
        var excludeTags = ParseTags(r.ExcludeTags);

        if (includeTags.Count > PostsQueryFilter.MaxTagsCount) {
            return ErrFactory.InvalidData(
                $"Cannot filter for more than {PostsQueryFilter.MaxTagsCount} tags set as included",
                $"Maximum amount of tags to include is {PostsQueryFilter.MaxTagsCount}. Current count is {includeTags.Count}"
            );
        }

        if (excludeTags.Count > PostsQueryFilter.MaxTagsCount) {
            return ErrFactory.InvalidData(
                $"Cannot filter for more than {PostsQueryFilter.MaxTagsCount} tags set as excluded",
                $"Maximum amount of tags to exclude is {PostsQueryFilter.MaxTagsCount}. Current count is {excludeTags.Count}"
            );
        }

        var invalidIncluded = includeTags.Where(t => !PostTagId.IsStringValidTag(t)).ToArray();
        if (invalidIncluded.Length > 0) {
            return ErrFactory.InvalidData(
                "Cannot filter because some of the tags specified as included are invalid",
                $"Invalid included tags: '{string.Join(", ", invalidIncluded)}'"
            );
        }

        var invalidExcluded = excludeTags.Where(t => !PostTagId.IsStringValidTag(t)).ToArray();
        if (invalidExcluded.Length > 0) {
            return ErrFactory.InvalidData(
                "Cannot filter because some of the tags specified as excluded are invalid",
                $"Invalid excluded tags: '{string.Join(", ", invalidExcluded)}'"
            );
        }

        var conflictingTags = includeTags.Intersect(excludeTags).ToArray();
        if (conflictingTags.Length > 0) {
            return ErrFactory.InvalidData(
                "Cannot filter because some tags are marked both as included and excluded",
                $"Conflicting tags: '{string.Join(", ", conflictingTags)}'"
            );
        }

        return new PostsQueryFilter(
            DateFrom: dateFrom,
            DateTo: dateTo,
            IncludeTags: includeTags.Select(t => PostTagId.Create(t).AsSuccess()).ToHashSet(),
            ExcludeTags: excludeTags.Select(t => PostTagId.Create(t).AsSuccess()).ToHashSet()
        );
    }

    private static HashSet<string> ParseTags(string? rawTags) => rawTags?
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Where(t => !string.IsNullOrWhiteSpace(t))
        .ToHashSet() ?? [];
}