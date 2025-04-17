using ApiShared;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.contracts.draft_posts;

public class UpdateDraftPostTagsRequest : IRequestWithValidationNeeded
{
    public string[] NewTags { get; init; } = [];

    public RequestValidationResult Validate() {
        // if( NewTags.Length > )
        return ErrFactory.NotImplemented();
    }
}