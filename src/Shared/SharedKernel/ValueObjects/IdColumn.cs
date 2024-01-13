namespace SharedKernel.ValueObjects;

public class IdColumn(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static IdColumn New => new(Guid.NewGuid());
    public static IdColumn None => new(Guid.Empty);

    public static implicit operator IdColumn(Guid value) => new (value);    
    public static implicit operator IdColumn(string value) => Guid.Parse(value);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}