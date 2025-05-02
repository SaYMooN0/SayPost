using System.Text.RegularExpressions;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.common;

public class DraftPostId(Guid value) : GuidBasedId(value)
{
    public static DraftPostId CreateNew() => new(Guid.CreateVersion7());
}

public class PostTagId : ValueObject, IComparable,  IEntityId
{
    public const int MaxTagLength = 30;

    public static readonly Regex TagRegex =
        new Regex(@"^[a-zA-Zа-яА-Я0-9\+\-_]{1," + MaxTagLength + "}$");

    public static bool IsStringValidTag(string tag) => TagRegex.IsMatch(tag);
    private string Value { get; }

    private PostTagId(string value) {
        if (!IsStringValidTag(value)) {
            throw new ErrCausedException(ErrFactory.IncorrectFormat($"'{value}' is not a valid tag"));
        }

        Value = value;
    }

    public static ErrOr<PostTagId> Create(string value) {
        if (!IsStringValidTag(value)) {
            return ErrFactory.IncorrectFormat($"'{value}' is not a valid tag");
        }

        return new PostTagId(value);
    }

    public override string ToString() => Value;
    public int CompareTo(object? obj)  => obj switch {
        IEntityId ed => ToString().CompareTo(ed.ToString()),
        Guid guid => guid.CompareTo(Value),
        _ => -1
    };

    public override IEnumerable<object> GetEqualityComponents() {
        yield return Value;
    }
}