using System.Text.RegularExpressions;

namespace Dashboard.Domain.Extensions;

internal static class PasswordGuardExt
{
    private static readonly Regex Regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z]).{6,}$");
    
    public static string Password(this IGuardClause guardClause, string input, string parameterName)
    {
        guardClause.NullOrWhiteSpace(input, parameterName);

        if (!Regex.IsMatch(input))
        {
            throw new ArgumentException("Password must contain at least one uppercase letter, one lowercase letter, and be at least 6 characters long.");
        }

        return input;
    }
}