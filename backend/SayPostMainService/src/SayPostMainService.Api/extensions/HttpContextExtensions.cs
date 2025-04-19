using SayPostMainService.Domain.common;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.extensions;

public static class HttpContextExtensions
{
    public static DraftPostId GetDraftPostIdFromRoute(this HttpContext context) {
        var postIdString = context.Request.RouteValues["draftPostId"]?.ToString() ?? "";
        if (!Guid.TryParse(postIdString, out var guid)) {
            throw new ErrCausedException(ErrFactory.InvalidData(
                "Invalid draft post id",
                "Couldn't parse draft post id from route"
            ));
        }

        return new DraftPostId(guid);
    }
}