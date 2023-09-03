namespace Agent.UI.Domain.Models;

public class TargetServiceDomain
{
    public string Domain{ get; private set; }

    public void SetDomain(string domain)
    {
        Domain = domain;
    }
};