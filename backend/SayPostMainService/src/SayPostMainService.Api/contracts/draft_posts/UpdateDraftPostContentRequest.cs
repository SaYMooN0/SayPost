using ApiShared;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.common.post_aggregates_shared.post_content;
using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

public class UpdateDraftPostContentRequest : IRequestWithValidationNeeded
{
    public string[] NewPostContent { get; init; } = [];

    public RequestValidationResult Validate() => RequestValidationResult.Success;

    public PostContent GetParsedPostContent() => PostContent.Default();
}