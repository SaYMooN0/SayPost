using ApiShared;
using ApiShared.extensions;
using SayPostMainService.Api.extensions;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.configs;

namespace SayPostMainService.Api.endpoints_filters;

internal class AccessToModifyDraftPostRequiredEndpointFilter : IEndpointFilter
{
    private readonly IDraftPostsRepository _draftPostsRepository;

    public AccessToModifyDraftPostRequiredEndpointFilter(IDraftPostsRepository draftPostsRepository) {
        _draftPostsRepository = draftPostsRepository;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        var httpContext = context.HttpContext;
        AppUserId currentUserId = httpContext.GetAuthenticatedUserId();
        DraftPostId postId = httpContext.GetDraftPostIdFromRoute();

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