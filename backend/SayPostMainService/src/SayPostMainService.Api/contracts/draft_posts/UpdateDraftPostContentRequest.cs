using ApiShared;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

public class UpdateDraftPostContentRequest : IRequestWithValidationNeeded
{
    public string NewPostContent { get; init; } = string.Empty;

    public RequestValidationResult Validate() =>
        PostContent.IsStringCorrectPostContent(NewPostContent).IsErr(out var err)
            ? err
            : RequestValidationResult.Success;

    public PostContent GetParsedPostContent() => PostContent.CreateFromString(NewPostContent).AsSuccess();
}