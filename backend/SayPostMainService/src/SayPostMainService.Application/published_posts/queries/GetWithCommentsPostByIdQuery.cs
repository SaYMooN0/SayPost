using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.published_posts.queries;

public record class GetWithCommentsPostByIdQuery(PublishedPostId Id) : IRequest<ErrOr<PublishedPost>>;

internal class GetWithCommentsPostByIdQueryHandler
    : IRequestHandler<GetWithCommentsPostByIdQuery, ErrOr<PublishedPost>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;

    public GetWithCommentsPostByIdQueryHandler(IPublishedPostsRepository publishedPostsRepository) {
        _publishedPostsRepository = publishedPostsRepository;
    }

    public async Task<ErrOr<PublishedPost>> Handle(
        GetWithCommentsPostByIdQuery request, CancellationToken cancellationToken
    ) {
        PublishedPost? post = await _publishedPostsRepository.AsNoTrackingWithCommentsById(request.Id);
        if (post is null) {
            return ErrFactory.NotFound($"Post with id {request.Id} was not found");
        }

        return post;
    }
}