using ApiShared;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.rules;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.contracts.draft_posts.update_tags;

public class UpdateDraftPostTagsRequest : IRequestWithValidationNeeded
{
    public string[] NewTags { get; init; } = [];

    public RequestValidationResult Validate() {
        if (NewTags.Length > PostsRules.MaxTagsForPostCount) {
            return PostsRules.TooManyTagsForPostSelectedErr(NewTags.Length);
        }

        var incorrectTags = NewTags.Where(t => !PostTagId.IsStringValidTag(t));
        if (incorrectTags.Any()) {
            return new ErrList(
                incorrectTags.Select(t => ErrFactory.IncorrectFormat($"'{t}' is not a valid tag"))
            );
        }

        return RequestValidationResult.Success;
    }

    public HashSet<PostTagId> GetParsedPostTags() => NewTags
        .Select(t => PostTagId.Create(t).AsSuccess())
        .ToHashSet();
}