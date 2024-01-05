namespace Dashboard.Domain.ValueObjects;

public class IdColumn(string value) : ValueObject
{
    public string Value { get; } = Guard.Against.NullOrEmpty(value);

    public static IdColumn New => new(Guid.NewGuid().ToString());
    public static IdColumn None => new(Guid.Empty.ToString());

    public static implicit operator IdColumn(string value) => new IdColumn(value);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return value;
    }
}