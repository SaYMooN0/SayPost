﻿using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.published_posts.queries;

public record class GetPostWithLikesQuery(PublishedPostId Id) : IRequest<ErrOr<PublishedPost>>;

internal class GetWithCommentsPostByIdQueryHandler
    : IRequestHandler<GetPostWithLikesQuery, ErrOr<PublishedPost>>
{
    private readonly IPublishedPostsRepository _publishedPostsRepository;

    public GetWithCommentsPostByIdQueryHandler(IPublishedPostsRepository publishedPostsRepository) {
        _publishedPostsRepository = publishedPostsRepository;
    }

    public async Task<ErrOr<PublishedPost>> Handle(
        GetPostWithLikesQuery request, CancellationToken cancellationToken
    ) {
        PublishedPost? post = await _publishedPostsRepository.GetByIdAsNoTracking(request.Id);
        if (post is null) {
            return ErrFactory.NotFound($"Post with id {request.Id} was not found");
        }

        return post;
    }
}