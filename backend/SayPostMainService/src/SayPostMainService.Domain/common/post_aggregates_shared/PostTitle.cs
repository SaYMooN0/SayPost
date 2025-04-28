using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common.post_aggregates_shared;

public class PostTitle : ValueObject
{
    public const int MaxContentLength = 512;
    private string Value { get; }

    private PostTitle(string value) {
        if (IsStringCorrectPostTitle(value).IsErr(out var err)) {
            throw new ErrCausedException(err);
        }

        Value = value;
    }

    public static PostTitle CreateNew() => new("New post");

    public static ErrOr<PostTitle> CreateFromString(string value) =>
        IsStringCorrectPostTitle(value).IsErr(out var err) ? err : new PostTitle(value);

    public override string ToString() => Value;

    public static ErrOrNothing IsStringCorrectPostTitle(string value) {
        if (string.IsNullOrWhiteSpace(value)) {
            return ErrFactory.InvalidData("Post title can't be empty");
        }

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