// See https://aka.ms/new-console-template for more information

using CMD_Test;

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
