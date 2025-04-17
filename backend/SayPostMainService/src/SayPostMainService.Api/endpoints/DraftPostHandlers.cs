using ApiShared;
using ApiShared.extensions;
using MediatR;
using SayPostMainService.Api.contracts.draft_posts;
using SayPostMainService.Api.extensions;
using SayPostMainService.Application.draft_posts.commands;
using SayPostMainService.Application.draft_posts.queries;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Api.endpoints;

internal static class DraftPostHandlers
{
    internal static IEndpointRouteBuilder MapDraftPostHandlers(this IEndpointRouteBuilder endpoints) {
        endpoints.MapGet("/", ListDraftPosts)
            .WithAuthenticationRequired();
        endpoints.MapPost("/create", CreateDraftPost)
            .WithAuthenticationRequired();

        endpoints.MapPatch("/{postId}/{updateTitle}", UpdateDraftPostTitle)
            .WithAuthenticationRequired()
            .WithRequestValidation<UpdateDraftPostTitleRequest>()
            .WithAccessToModifyDraftPostRequired();

        return endpoints;
    }

    private static async Task<IResult> ListDraftPosts(
        HttpContext httpContext, ISender mediator, string sortOption = "lastModified"
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();

        var query = new ListDraftPostsForUserQuery(userId, sortOption);
        var result = await mediator.Send(query);

        return CustomResults.FromErrOr(result,
            (posts) => Results.Json(ListDraftPostMainInfoResponse.FromPosts(posts))
        );
    }

    private static async Task<IResult> CreateDraftPost(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();

        var command = new CreateNewDraftPostCommand(userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (dp) => Results.Json(DraftPostFullInfoResponse.FromDraftPost(dp))
        );
    }

    private static async Task<IResult> UpdateDraftPostTitle(
        HttpContext httpContext, ISender mediator
    ) {
        AppUserId userId = httpContext.GetAuthenticatedUserId();
        var request = httpContext.RequestServices.GetService<UpdateDraftPostTitleRequest>();

        var command = new CreateNewDraftPostCommand(userId);
        var result = await mediator.Send(command);

        return CustomResults.FromErrOr(result,
            (dp) => Results.Json(DraftPostFullInfoResponse.FromDraftPost(dp))
        );
    }
}