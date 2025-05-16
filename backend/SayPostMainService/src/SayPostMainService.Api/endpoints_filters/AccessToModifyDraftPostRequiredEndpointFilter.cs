using ApiShared;
using ApiShared.extensions;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.endpoints_filters;

internal class AccessToModifyDraftPostRequiredEndpointFilter : IEndpointFilter
{
    private readonly IDraftPostsRepository _draftPostsRepository;

    public AccessToModifyDraftPostRequiredEndpointFilter(IDraftPostsRepository draftPostsRepository) {
        _draftPostsRepository = draftPostsRepository;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        var httpContext = context.HttpContext;
        
        var postIdString = httpContext.Request.RouteValues["draftPostId"]?.ToString() ?? "";
        if (!Guid.TryParse(postIdString, out var postGuid)) {
            return CustomResults.ErrorResponse(ErrFactory.InvalidData(
                "Invalid draft post id",
                $"Couldn't parse draft post id from route. Given value: {postIdString}"
            ));
        }
        
        DraftPostId postId = new(postGuid);
        AppUserId currentUserId = httpContext.GetAuthenticatedUserId();

        ErrOr<AppUserId> postAuthorGetRes = await _draftPostsRepository.GetPostAuthor(postId);
        if (postAuthorGetRes.IsErr(out var err)) {
            return CustomResults.ErrorResponse(err);
        }

        if (postAuthorGetRes.AsSuccess() != currentUserId) {
            return CustomResults.ErrorResponse(ErrFactory.NoAccess(
                "You cannot modify this draft post",
                "You need to be author og the post"
                ));
        }

        return await next(context);
    }
}