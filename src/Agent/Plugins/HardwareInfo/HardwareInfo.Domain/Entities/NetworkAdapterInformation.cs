namespace HardwareInfo.Domain.Entities;

public record NetworkAdapterInformation(string Name, string Type, string MacAddress, IList<string> Ip,
    string Dhcp, IList<string> Dns, IList<string> Subnet);