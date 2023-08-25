﻿// See https://aka.ms/new-console-template for more information

using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Hardware.Info;

Console.WriteLine(RuntimeInformation.OSDescription);
Console.WriteLine(RuntimeInformation.FrameworkDescription);
Console.WriteLine(RuntimeInformation.RuntimeIdentifier);
Console.WriteLine(Environment.MachineName);
Console.WriteLine(Environment.UserName);
Console.WriteLine(Environment.SystemDirectory);
Console.WriteLine(Environment.OSVersion);
Console.WriteLine(string.Join(',', Environment.GetLogicalDrives()));


Console.WriteLine(RuntimeInformation.ProcessArchitecture);
Console.WriteLine(RuntimeInformation.OSArchitecture);
RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

// var startCpuUsage = Process.GetProcesses().Sum(a => a.TotalProcessorTime.TotalMilliseconds);
//
// Console.WriteLine(startCpuUsage);


Console.WriteLine("Hello, World!");
IHardwareInfo hardwareInfo = new HardwareInfo();
//hardwareInfo.RefreshAll();

hardwareInfo.RefreshOperatingSystem();
Console.WriteLine(hardwareInfo.OperatingSystem);

hardwareInfo.RefreshMemoryStatus();
Console.WriteLine(hardwareInfo.MemoryStatus);

/*foreach (var hardware in hardwareInfo.BatteryList)
    Console.WriteLine(hardware);*/

hardwareInfo.RefreshBIOSList();
foreach (var hardware in hardwareInfo.BiosList)
    Console.WriteLine(hardware);

hardwareInfo.RefreshCPUList();
foreach (var cpu in hardwareInfo.CpuList)
{
    Console.WriteLine(cpu);

    foreach (var cpuCore in cpu.CpuCoreList)
        Console.WriteLine(cpuCore);
}

/*hardwareInfo.RefreshDriveList();
foreach (var drive in hardwareInfo.DriveList)
{
    Console.WriteLine(drive);

    foreach (var partition in drive.PartitionList)
    {
        Console.WriteLine(partition);

        foreach (var volume in partition.VolumeList)
            Console.WriteLine(volume);
    }
}*/

/*foreach (var hardware in hardwareInfo.KeyboardList)
    Console.WriteLine(hardware);*/

hardwareInfo.RefreshMemoryList();
foreach (var hardware in hardwareInfo.MemoryList)
    Console.WriteLine(hardware);


/*foreach (var hardware in hardwareInfo.MonitorList)
    Console.WriteLine(hardware);*/

hardwareInfo.RefreshMotherboardList();
foreach (var hardware in hardwareInfo.MotherboardList)
    Console.WriteLine(hardware);

/*foreach (var hardware in hardwareInfo.MouseList)
    Console.WriteLine(hardware);*/

hardwareInfo.RefreshNetworkAdapterList();
foreach (var hardware in hardwareInfo.NetworkAdapterList)
{
    Console.WriteLine(hardware);
    Console.WriteLine("IP: "+string.Join(',', hardware.IPAddressList));
    Console.WriteLine("DHCP: "+string.Join(',', hardware.DHCPServer));
    Console.WriteLine("DNS: "+string.Join(',', hardware.DNSServerSearchOrderList));
    Console.WriteLine("Subnet: "+string.Join(',', hardware.IPSubnetList));
    Console.WriteLine();
}


/*foreach (var hardware in hardwareInfo.PrinterList)
    Console.WriteLine(hardware);

foreach (var hardware in hardwareInfo.SoundDeviceList)
    Console.WriteLine(hardware);

foreach (var hardware in hardwareInfo.VideoControllerList)
    Console.WriteLine(hardware);*/

//Console.ReadLine();

/*foreach (var address in HardwareInfo.GetLocalIPv4Addresses(NetworkInterfaceType.Ethernet, OperationalStatus.Up))
    Console.WriteLine(address);

Console.WriteLine();

foreach (var address in HardwareInfo.GetLocalIPv4Addresses(NetworkInterfaceType.Wireless80211))
    Console.WriteLine(address);

Console.WriteLine();

foreach (var address in HardwareInfo.GetLocalIPv4Addresses(OperationalStatus.Up))
    Console.WriteLine(address);

Console.WriteLine();

foreach (var address in HardwareInfo.GetLocalIPv4Addresses())
    Console.WriteLine(address);

//Console.ReadLine();*/