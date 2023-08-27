namespace HardwareInfo.Domain.Entities;

public record NetworkAdapterInformation(string Id, string Name, string Type, string MacAddress, IList<string> Ip,
    string Dhcp, IList<string> Dns, long Speed);