using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common.post_aggregates_shared;

public class PostContent : ValueObject
{
    public const int MaxContentLength = 5096;
    private string Value { get; }

    private PostContent(string value) {
        if (IsStringCorrectPostContent(value).IsErr(out var err)) {
            throw new ErrCausedException(err);
        }

        Value = value;
    }

    public static PostContent CreateNew() => new("New post content");

    public static ErrOr<PostContent> CreateFromString(string value) =>
        IsStringCorrectPostContent(value).IsErr(out var err) ? err : new PostContent(value);

    public override string ToString() => Value;

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