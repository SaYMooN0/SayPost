using SayPostMainService.Domain.common;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.extensions;

public static class HttpContextExtensions
{
    public static DraftPostId GetDraftPostIdFromRoute(this HttpContext context) {
        var postIdString = context.Request.RouteValues["draftPostId"]?.ToString() ?? "";
        if (!Guid.TryParse(postIdString, out var guid)) {
            throw new ErrCausedException(ErrFactory.InvalidData(
                "Invalid draft post id",
                $"Couldn't parse draft post id from route. Given value: {postIdString}"
            ));
        }

        return new DraftPostId(guid);
    }
    public static PublishedPostId GetPublishedPostIdFromRoute(this HttpContext context) {
        var postIdString = context.Request.RouteValues["postId"]?.ToString() ?? "";
        if (!Guid.TryParse(postIdString, out var guid)) {
            throw new ErrCausedException(ErrFactory.InvalidData(
                "Invalid post id",
                $"Couldn't parse post id from route. Given value: {postIdString}"
            ));
        }

        return new PublishedPostId(guid);
    }
    
}