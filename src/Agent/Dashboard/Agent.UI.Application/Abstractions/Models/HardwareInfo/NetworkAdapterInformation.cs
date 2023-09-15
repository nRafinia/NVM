namespace Agent.UI.Application.Abstractions.Models.HardwareInfo;

public record NetworkAdapterInformation(string Id, string Name, string Type, string MacAddress, IList<string> Ip,
    string Dhcp, IList<string> Dns, long Speed);