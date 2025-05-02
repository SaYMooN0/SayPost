using ApiShared;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.endpoints_filters;

public class EnsurePostExistsRequiredEndpointFilter : IEndpointFilter
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;

    public EnsurePostExistsRequiredEndpointFilter(IPublishedPostsRepository publishedPostsRepository) {
        _publishedPostsRepository = publishedPostsRepository;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
        var httpContext = context.HttpContext;

        var postIdString = httpContext.Request.RouteValues["postId"]?.ToString() ?? "";
        if (!Guid.TryParse(postIdString, out var postGuid)) {
            return CustomResults.ErrorResponse(ErrFactory.InvalidData(
                "Invalid post id",
                $"Couldn't parse post id from route. Given value: {postIdString}"
            ));
        }

        PublishedPostId postId = new(postGuid);
        if (!await _publishedPostsRepository.AnyPostWithId(postId)) {
            return CustomResults.ErrorResponse(ErrFactory.NotFound(
                "Post does not exist",
                $"Post with id: {postId} does not exist"
            ));
        }


        return await next(context);
    }
}