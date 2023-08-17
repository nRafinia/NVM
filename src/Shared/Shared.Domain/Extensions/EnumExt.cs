namespace Shared.Domain.Shared.Extensions;

internal static class EnumExt
{
    public static T ToEnum<T>(this JsonNode node)
        where T : struct, IConvertible
    {
        return typeof(T).IsEnum
            ? (T)Enum.Parse(typeof(T), node.ToString())
            : default;
    }

 
    
}