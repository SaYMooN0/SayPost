namespace SharedKernel.common.domain.ids;

public abstract class GuidBasedId : ValueObject, IComparable, IEntityId
{
    public Guid Value { get; }
    protected GuidBasedId(Guid value) => Value = value;
    public override string ToString() => Value.ToString();

    public override IEnumerable<object> GetEqualityComponents() {
        yield return Value;
    }

    public int CompareTo(object? obj) => obj switch {
        IEntityId ed => ToString().CompareTo(ed.ToString()),
        Guid guid => guid.CompareTo(Value),
        _ => -1
    };
}