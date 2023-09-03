// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using CMD_Test;

/*var nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
// Select desired NIC
var nic = nics.Single(n => n.Name == "Wi-Fi");
var reads = Enumerable.Empty<double>();
var sw = new Stopwatch();
var lastBr = nic.GetIPv4Statistics().BytesReceived;
for (var i = 0; i < 1000; i++) {

    sw.Restart();
    Thread.Sleep(100);
    var elapsed = sw.Elapsed.TotalSeconds;
    var br = nic.GetIPv4Statistics().BytesReceived;

    var local = (br - lastBr) / elapsed;
    lastBr = br;

    // Keep last 20, ~2 seconds
    reads = new [] { local }.Concat(reads).Take(20);

    if (i % 10 == 0) { // ~1 second
        var bSec = reads.Sum() / reads.Count();
        var kbs = (bSec * 8) / 1024; 
        Console.WriteLine("Kb/s ~ " + kbs);
    }
}
return;*/
ShowNetworkInterfaces();
return;
var n=NetworkInterface.GetAllNetworkInterfaces();

Console.WriteLine("OS: " + Environment.OSVersion);

var process = new RunCommand();

var inputStream = process.RunProcess("cmd", "", Console.WriteLine, Console.WriteLine);
//var inputStream = process.RunProcess("powershell", "", Console.WriteLine, s => Console.WriteLine(s));
//var inputStream = process.RunProcess("bash", "", Console.WriteLine, s => Console.WriteLine(s));


//Console.Write("Command> ");
var command = Console.ReadLine();
if (string.IsNullOrWhiteSpace(command) || process.IsExited)
{
    return;
}

do
{
    //RunCommand.RunProcess("ping", "8.8.8.8", Console.WriteLine, s => Console.WriteLine("Err->" + s));

    inputStream.Invoke(command!);
    if (process.IsExited)
    {
        break;
    }

    //Console.Write("Command> ");
    command = Console.ReadLine();
} while (!string.IsNullOrWhiteSpace(command) || process.IsExited);

process.Dispose();

Console.WriteLine("Finished.");


static void ShowNetworkInterfaces()
{
    IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    Console.WriteLine("Interface information for {0}.{1}     ",
            computerProperties.HostName, computerProperties.DomainName);
    if (nics == null || nics.Length < 1)
    {
        Console.WriteLine("  No network interfaces found.");
        return;
    }

    Console.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
    foreach (NetworkInterface adapter in nics)
    {
        IPInterfaceProperties properties = adapter.GetIPProperties();
        Console.WriteLine();
        Console.WriteLine(adapter.Description);
        Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length,'='));
        Console.WriteLine("  name .................................... : {0}", adapter.Name);
        Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
        Console.WriteLine("  Physical Address ........................ : {0}",
                   adapter.GetPhysicalAddress().ToString());
        Console.WriteLine("  Operational status ...................... : {0}",
            adapter.OperationalStatus);        
        Console.WriteLine("  Speed ................................... : {0}", adapter.Speed);
        string versions ="";

        // Create a display string for the supported IP versions.
        if (adapter.Supports(NetworkInterfaceComponent.IPv4))
        {
             versions = "IPv4";
         }
        if (adapter.Supports(NetworkInterfaceComponent.IPv6))
        {
            if (versions.Length > 0)
            {
                versions += " ";
             }
            versions += "IPv6";
        }
        Console.WriteLine("  IP version .............................. : {0}", versions);
        ShowIPAddresses(properties);

        // The following information is not useful for loopback adapters.
        if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
        {
            continue;
        }
        Console.WriteLine("  DNS suffix .............................. : {0}",
            properties.DnsSuffix);

        if (adapter.Supports(NetworkInterfaceComponent.IPv4))
        {
            IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
            Console.WriteLine("  MTU...................................... : {0}", ipv4.Mtu);

        }

        /*Console.WriteLine("  DNS enabled ............................. : {0}",
            properties.IsDnsEnabled);
        Console.WriteLine("  Dynamically configured DNS .............. : {0}",
            properties.IsDynamicDnsEnabled);*/
        Console.WriteLine("  Receive Only ............................ : {0}",
            adapter.IsReceiveOnly);
        Console.WriteLine("  Multicast ............................... : {0}",
            adapter.SupportsMulticast);
        ShowInterfaceStatistics(adapter);
        

        Console.WriteLine();
    }
}

static void ShowIPAddresses(IPInterfaceProperties properties)
{
    Console.WriteLine("  IP ...............................");
    for (var i = 0; i < properties.UnicastAddresses.Count; i++)
    {
        var ipAddress = properties.UnicastAddresses[i];
        Console.WriteLine("    {0} ............................... : {1}/{2}",
            i+1, ipAddress.Address, 
            ipAddress.Address.AddressFamily!=AddressFamily.InterNetworkV6? ipAddress.IPv4Mask:"");
    }    
    
    Console.WriteLine("  Gateway ...............................");
    for (var i = 0; i < properties.GatewayAddresses.Count; i++)
    {
        Console.WriteLine("    {0} ............................... : {1}",
            i+1, properties.GatewayAddresses[i].Address);
    }     
    
    Console.WriteLine("  DNS ...............................");
    for (var i = 0; i < properties.DnsAddresses.Count; i++)
    {
        Console.WriteLine("    {0} ............................... : {1}",
            i+1, properties.DnsAddresses[i]);
    }    
    
    Console.WriteLine("  DHCP ...............................");
    for (var i = 0; i < properties.DhcpServerAddresses.Count; i++)
    {
        Console.WriteLine("    {0} ............................... : {1}",
            i+1, properties.DhcpServerAddresses[i]);
    }
}

static void ShowInterfaceStatistics(NetworkInterface adapter)
{
    Console.WriteLine("  Byte received ........................... : {0}",
        adapter.GetIPStatistics().BytesReceived);
    Console.WriteLine("  Byte sent ............................... : {0}",
        adapter.GetIPStatistics().BytesSent);
}