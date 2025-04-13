using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.draft_post_aggregate;

public class DraftPostTitle : ValueObject
{
    public const int MaxContentLength = 512;
    private DraftPostTitle() { }
    private string Value { get; set; }

    public static DraftPostTitle CreateNew() => new() {
        Value = "New post"
    };

    public override string ToString() => Value;

    public ErrOrNothing Update(string newValue) {
        if (IsStringCorrectPostTitle(newValue).IsErr(out var err)) {
            return err;
        }

        Value = newValue;
        return ErrOrNothing.Nothing;
    }

    public static ErrOrNothing IsStringCorrectPostTitle(string value) {
        if (value.Length > MaxContentLength) {
            return ErrFactory.InvalidData(
                "Content title is too long",
                $"New title length is {value.Length} characters. Maximum allowed length is {MaxContentLength}"
            );
        }

        return ErrOrNothing.Nothing;
    }

    public override IEnumerable<object> GetEqualityComponents() {
        yield return Value;
    }
}