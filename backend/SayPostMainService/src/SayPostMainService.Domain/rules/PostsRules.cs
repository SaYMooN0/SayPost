using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.rules;

public static class PostsRules
{
    public const int MaxTagsForPostCount = 250;

    public static Err TooManyTagsForPostSelectedErr(int selectedTagsCount) =>
        ErrFactory.LimitExceeded(
            $"Too many tags for post selected. Maximum allowed tags count for post is {MaxTagsForPostCount}",
            $"Selected {selectedTagsCount} tags. Post cannot have more than {MaxTagsForPostCount} "
        );
}