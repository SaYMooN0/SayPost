using SayPostMainService.Domain.common;
using SharedKernel.common.domain;

namespace SayPostMainService.Domain.post_tag_aggregate;

public class PostTag : AggregateRoot<PostTagId>
{
    private PostTag() { }
    public int PostWithThisTagCount { get; private set; }
    public static PostTag Create(PostTagId postTagId) => new() {
        Id = postTagId,
        PostWithThisTagCount = 0
    };
    public override string ToString() => Id.ToString();
    public void IncrementPostWithThisTagCount() => PostWithThisTagCount++;
    public void DecrementPostWithThisTagCount() => PostWithThisTagCount--;
}