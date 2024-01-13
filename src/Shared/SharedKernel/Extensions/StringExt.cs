using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace SharedKernel.Extensions
{
    public static class StringExt
    {
        public static string Masked(this string source, char maskValue = '*')
            => source.Masked(maskValue, 1, source.Length - 2);


        public static string SetEmptyIfNull(this string value)
            => string.IsNullOrWhiteSpace(value) ? string.Empty : value;

        public static string SetDefaultIfNull(this string value, string defaultValue = "-") =>
            string.IsNullOrWhiteSpace(value) ? defaultValue : value;

        public static string DefaultIfEmpty(this string value, string defaultValue = "")
            => SetDefaultIfNull(value, defaultValue);

        public static dynamic DefaultIfNotPrice(this string value, dynamic defaultValue)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, new NumberFormatInfo(), out var dv))
            {
                return dv; //Convert.ToDecimal(value);
            }

            if (int.TryParse(value, out var iv))
            {
                return iv; //Convert.ToInt32(value);
            }

            return defaultValue;
        }

        public static int DefaultIfNotInt(this string value, int defaultValue = 0)
        {
            var res = defaultValue;

            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out _))
            {
                res = int.Parse(value);
            }

            return res;
        }

        public static decimal DefaultIfNotDecimal(this string value, decimal defaultValue = 0)
        {
            var res = defaultValue;

            if (!string.IsNullOrWhiteSpace(value) && decimal.TryParse(value, NumberStyles.AllowDecimalPoint,
                    new NumberFormatInfo(), out var v))
            {
                res = v;
            }

            return res;
        }

        public static string Masked(this string source, char maskValue, int start, int count)
        {
            var firstPart = source[..start];
            var lastPart = source[(start + count)..];
            var middlePart = new string(maskValue, count);

            return firstPart + middlePart + lastPart;
        }

        private static string TextWithStar(this string txt)
        {
            if (txt == "")
            {
                return "";
            }

            var result = "";
            result += txt[0];
            for (var i = 1; i < txt.Length - 1; i++)
            {
                result += " * ";
            }

            result += txt[^1];
            return result;
        }

        public static string SafeSubString(this string text, int startIndex, int length)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            if (length > text.Length)
                length = text.Length - startIndex;

            return startIndex > length
                ? text
                : text.Substring(startIndex, length);
        }

        public static string SubStr(this string value, int startIndex, int endIndex)
        {
            var r = new Range(startIndex, endIndex);
            return value[r];
        }

        public static bool IsValidBillId(this string billId)
        {
            if (billId.Length < 6)
            {
                return false;
            }

            long sum = 0;
            var p = 2;
            for (var i = billId.Length - 2; i >= 0; i--)
            {
                sum += long.Parse(billId[i].ToString()) * p;
                p++;
                if (p > 7) p = 2;
            }

            var mod = sum % 11;
            mod = mod > 1 ? 11 - mod : 0;
            if (billId[^1].ToString() != mod.ToString())
                return false;

            return true;
        }

        public static string GetFieldDisplayName(this object obj)
        {
            var displayAttributeType = typeof(DisplayAttribute);

            var field = obj.GetType().GetField(obj.ToString() ?? string.Empty);

            if (field == null)
            {
                return obj.ToString() ?? string.Empty;
            }

            return field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault() is not DisplayAttribute
                attributes
                ? obj.ToString() ?? string.Empty
                : attributes.GetName() ?? string.Empty;
        }

        public static string GetPropertyDisplayName(this object obj)
        {
            var displayAttributeType = typeof(DisplayAttribute);

            var field = obj.GetType().GetProperty(obj.ToString() ?? string.Empty);

            if (field == null)
            {
                return obj.ToString() ?? string.Empty;
            }

            var attributes =
                field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault() as DisplayAttribute;

            return attributes is null
                ? obj.ToString() ?? string.Empty
                : attributes.GetName() ?? string.Empty;
        }

        public static string GetDisplayName(this PropertyInfo obj)
        {
            var displayAttributeType = typeof(DisplayAttribute);

            return obj.GetCustomAttributes(displayAttributeType, false).FirstOrDefault() is not DisplayAttribute
                attributes
                ? obj.ToString() ?? string.Empty
                : attributes.GetName() ?? string.Empty;
        }

    }
}