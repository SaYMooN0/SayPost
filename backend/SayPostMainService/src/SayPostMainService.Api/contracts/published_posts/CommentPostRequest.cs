using ApiShared;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.contracts.published_posts;

public class CommentPostRequest : IRequestWithValidationNeeded
{
    public string Content { get; init; }

    public RequestValidationResult Validate() {
        if (string.IsNullOrWhiteSpace(Content)) {
            return ErrFactory.NoValue("Comment text cannot be empty");
        }

        if (Content.Length > PostComment.MaxCommentLength) {
            return ErrFactory.NoValue(
                $"Comment text cannot be longer than { PostComment.MaxCommentLength}",
                $"Current comment text length is {Content.Length}"
            );
        }
        return RequestValidationResult.Success;
    }
}