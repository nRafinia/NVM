using System.Text;

namespace Dashboard.Domain.Licenses;

public class License
{
    public LicenseType Type { get; init; } = LicenseType.Free;
    public string Company { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateOnly ExpirationDate { get; init; } = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
    public string Key { get; init; } = string.Empty;

    public override string ToString()
    {
        var result = new StringBuilder();
        result.AppendLine("License detail:");
        result.AppendLine($"Type: {Type}");
        result.AppendLine($"Company: {Company}");
        result.AppendLine($"Name: {Name}");
        result.AppendLine($"Email: {Email}");
        result.AppendLine($"ExpirationDate: {ExpirationDate}");
        
        return result.ToString();
    }
}