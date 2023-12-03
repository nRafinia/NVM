using System.Globalization;
using System.Text.RegularExpressions;

namespace SharedKernel.Shared;

public static class SizeConverter
{
    private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB" };

    public static string ConvertBytesToString(long bytes)
    {
        if (bytes < 0)
        {
            return "-" + ConvertBytesToString(-bytes);
        }

        var index = 0;
        var dValue = (decimal)bytes;
        while (Math.Round(dValue, 1) >= 1000)
        {
            dValue /= 1024;
            index++;
        }

        return $"{dValue:n1} {SizeSuffixes[index]}";
    }

    public static long ConvertStringToBytes(string sizeString)
    {
        sizeString = sizeString.Trim();
        var pattern = new Regex(@"(\d+\.?\d*)([KMGTB])");
        var match = pattern.Match(sizeString);

        if (!match.Success)
        {
            return -1;
        }

        var valueString = match.Groups[1].Value;
        var suffix = match.Groups[2].Value;

        var value = double.Parse(valueString);
        long multiplier = 1;

        if (!string.IsNullOrEmpty(suffix))
        {
            multiplier = suffix switch
            {
                "K"=> 1024, 
                "M"=> 1048576, 
                "G"=> 1073741824, 
                "T"=> 1099511627776,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return long.Parse(Math.Round(value * multiplier).ToString(CultureInfo.InvariantCulture));
    }
}