using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.draft_post_aggregate;

public class DraftPostTitle : ValueObject
{
    public const int MaxContentLength = 512;
    private string Value { get; }

    private DraftPostTitle(string value) {
        if (IsStringCorrectPostTitle(value).IsErr(out var err)) {
            throw new ErrCausedException(err);
        }

        Value = value;
    }

    public static DraftPostTitle CreateNew() => new("New post");

    public static ErrOr<DraftPostTitle> CreateFromString(string value) =>
        IsStringCorrectPostTitle(value).IsErr(out var err) ? err : new DraftPostTitle(value);

    public override string ToString() => Value;

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