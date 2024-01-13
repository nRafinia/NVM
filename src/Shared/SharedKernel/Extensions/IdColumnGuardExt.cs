using SharedKernel.ValueObjects;

namespace SharedKernel.Extensions;

public static class IdColumnGuardExt
{
    public static IdColumn IdColumn(this IGuardClause guardClause, Guid input, string parameterName)
    {
        guardClause.Default(input, parameterName);

        if (input == ValueObjects.IdColumn.None.Value)
        {
            throw new ArgumentException("IdColumn must not be None.");
        }

        return input;
    }
    
    public static IdColumn IdColumn(this IGuardClause guardClause, IdColumn input, string parameterName)
    {
        guardClause.Default(input.Value, parameterName);

        if (input == ValueObjects.IdColumn.None)
        {
            throw new ArgumentException("IdColumn must not be None.");
        }

        return input;
    }
}