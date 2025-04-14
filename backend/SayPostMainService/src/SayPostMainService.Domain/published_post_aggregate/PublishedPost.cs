using SayPostMainService.Domain.common;
using SharedKernel.common.domain;

namespace SayPostMainService.Domain.published_post_aggregate;

public class PublishedPost : AggregateRoot<PublishedPostId>
{
    private PublishedPost() { }
}