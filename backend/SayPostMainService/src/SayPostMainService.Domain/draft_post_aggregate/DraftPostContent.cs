using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.draft_post_aggregate;

public class DraftPostContent : ValueObject
{
    public const int MaxContentLength = 5096;
    private DraftPostContent() { }
    private string Value { get; set; }

    public static DraftPostContent CreateNew() => new() {
        Value = "New post content"
    };

    public override string ToString() => Value;

    public ErrOrNothing Update(string newValue) {
        if (IsStringCorrectPostContent(newValue).IsErr(out var err)) {
            return err;
        }

        Value = newValue;
        return ErrOrNothing.Nothing;
    }

    public static ErrOrNothing IsStringCorrectPostContent(string value) {
        if (value.Length > MaxContentLength) {
            return ErrFactory.InvalidData(
                "Content length is too long",
                $"New content length is {value.Length} characters. Maximum allowed length is {MaxContentLength}"
            );
        }

        return ErrOrNothing.Nothing;
    }

    public override IEnumerable<object> GetEqualityComponents() {
        yield return Value;
    }
}