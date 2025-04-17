using ApiShared;
using SayPostMainService.Domain.draft_post_aggregate;

namespace SayPostMainService.Api.contracts.draft_posts;

public class UpdateDraftPostTitleRequest : IRequestWithValidationNeeded
{
    public string NewPostTitle { get; init; } = string.Empty;

    public RequestValidationResult Validate() =>
        DraftPostTitle.IsStringCorrectPostTitle(NewPostTitle).IsErr(out var err)
            ? err
            : RequestValidationResult.Success;

    public DraftPostTitle GetParsedPostTitle() => DraftPostTitle.CreateFromString(NewPostTitle).AsSuccess();
}