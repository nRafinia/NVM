namespace Agent.UI.Domain.Models;

public class TargetServiceDomain
{
    public string Domain { get; private set; } = string.Empty;

    public bool IsReady => !string.IsNullOrWhiteSpace(Domain);

    public void SetDomain(string domain)
    {
        Domain = domain;
    }
};