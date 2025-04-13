using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.draft_posts;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.endpoints;

internal static class DraftPostHandlers
{
    internal static IEndpointRouteBuilder MapDraftPostHandlers(this IEndpointRouteBuilder endpoints) {
        endpoints.MapGet("/", ListDraftPosts);
        endpoints.MapPost("/create", CreateDraftPost);

        return endpoints;
    }

    private static async Task<IResult> ListDraftPosts(
        HttpContext httpContext, ISender mediator
    ) {
//GetAuthenticatedUserId
        return CustomResults.ErrorResponse(ErrFactory.NotImplemented());
    }

    private static async Task<IResult> CreateDraftPost(
        HttpContext httpContext, ISender mediator
    ) {
//GetAuthenticatedUserId
        return CustomResults.ErrorResponse(ErrFactory.NotImplemented());
    }
}