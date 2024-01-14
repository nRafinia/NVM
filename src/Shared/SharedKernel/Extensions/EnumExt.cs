using System.ComponentModel.DataAnnotations;

namespace SharedKernel.Extensions
{
    public static class EnumExt
    {
        public static string GetEnumName<T>(int v)
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                return string.Empty;
            }

            var source = Enum.GetValues(typeof(T));
            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in source)
            {
                if (value is null)
                {
                    continue;
                }

                var field = value.GetType().GetField(value.ToString()!);

                if (field is null)
                {
                    continue;
                }

                var attributes =
                    field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault() as DisplayAttribute;
                if ((int)value == v)
                {
                    return attributes is not null ? attributes.GetName() ?? string.Empty : value.ToString()!;
                }
            }

            return string.Empty;
        }

        public static string GetEnumName<T>(this T v)
            where T : struct
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                return "";
            }

            var field = v.GetType().GetField(v.ToString()!);

            if (field == null)
            {
                return v.ToString()!;
            }

            return field.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() is DisplayAttribute
                attributes
                ? attributes.GetName() ?? string.Empty
                : v.ToString()!;
        }

        /*public static Dictionary<int, string> GetEnumList<T>()
        {
            var items = new Dictionary<int, string>();
            var enumType = typeof(T);

            if (!enumType.IsEnum) return items;

            var source = Enum.GetValues(typeof(T));

            foreach (var value in source)
            {
                items.Add((int)value, value.GetFieldDisplayName());
            }

            return items;
        }*/

        public static Dictionary<TValue, string> GetEnumList<TValue>()
            where TValue : notnull
        {
            var items = new Dictionary<TValue, string>();
            var enumType = typeof(TValue);

            if (!enumType.IsEnum)
            {
                return items;
            }

            var source = Enum.GetValues(typeof(TValue));
            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in source)
            {
                if (value is null)
                {
                    continue;
                }

                var field = value.GetType().GetField(value.ToString() ?? string.Empty);

                if (field == null)
                {
                    continue;
                }

                var attributes =
                    field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault() as DisplayAttribute;

                items.Add(
                    (TValue)value,
                    attributes is not null
                        ? attributes.GetName() ?? string.Empty
                        : value.ToString() ?? string.Empty
                );
            }

            return items;
        }

        public static T GetEnumByName<T>(string name, T defaultValue)
        {
            T item;
            try
            {
                item = (T)Enum.Parse(typeof(T),
                    Enum.GetNames(typeof(T))
                        .First(e => String.Equals(e, name, StringComparison.CurrentCultureIgnoreCase)));
            }
            catch (Exception)
            {
                item = defaultValue;
            }

            return item;
        }

        public static T? GetEnumFromInt<T>(int number)
        {
            if (Enum.IsDefined(typeof(T), number))
            {
                return (T)Enum.ToObject(typeof(T), number);
            }

            return default;
        }

        public static T GetEnumByIndex<T>(int index, T defaultValue)
        {
            if (index < 0)
            {
                return defaultValue;
            }

            var items = Enum.GetNames(typeof(T));
            if (index > items.Length)
            {
                return defaultValue;
            }

            return (T)Enum.Parse(typeof(T),
                Enum.GetNames(typeof(T)).GetValue(index)!.ToString()!);
        }

        /// <summary>
        /// Check that value is in enum list
        /// </summary>
        /// <param name="value"></param>
        /// <returns>bool</returns>
        public static bool IsValidEnum(this Enum value)
        {
            return value.GetType().GetField(value.ToString()) != null;
        }
    }
}