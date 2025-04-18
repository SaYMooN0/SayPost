using ApiShared;
using SayPostMainService.Domain.common.post_aggregates_shared;
using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

public class UpdateDraftPostTitleRequest : IRequestWithValidationNeeded
{
    public string NewPostTitle { get; init; } = string.Empty;

    public RequestValidationResult Validate() =>
        PostTitle.IsStringCorrectPostTitle(NewPostTitle).IsErr(out var err)
            ? err
            : RequestValidationResult.Success;

    public PostTitle GetParsedPostTitle() => PostTitle.CreateFromString(NewPostTitle).AsSuccess();
}