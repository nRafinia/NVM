namespace Agent.UI.Helpers;

public static class FileSizeFormatter
{
    // Load all suffixes in an array
    private static readonly string[] Suffixes =
    {
        "Bytes", "KB", "MB", "GB", "TB", "PB"
    };

    public static string FormatSize(this int bytes)
    {
        var counter = 0;
        var number = (decimal)bytes;
        while (Math.Round(number / 1024) >= 1)
        {
            number /= 1024;
            counter++;
        }

        return $"{number:n1} {Suffixes[counter]}";
    }    
    
    public static string FormatSize(this long bytes)
    {
        var counter = 0;
        var number = (decimal)bytes;
        while (Math.Round(number / 1024) >= 1)
        {
            number /= 1024;
            counter++;
        }

        return $"{number:n1}{Suffixes[counter]}";
    }
}