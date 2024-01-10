using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel.ValueObjects;

namespace SharedKernel.Persistence.Converters;

public class IdColumnConverter: ValueConverter<IdColumn, string>
{
    public IdColumnConverter()
        : base(v => v.Value, v => v)
    {
    }
}