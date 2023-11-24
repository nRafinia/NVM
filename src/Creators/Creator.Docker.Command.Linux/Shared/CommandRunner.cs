using System.Diagnostics;
using Creator.Docker.Command.Linux.Errors;
using Microsoft.Extensions.Logging;
using Shared.Domain.Base.Results;

namespace Creator.Docker.Command.Linux.Shared;

internal class CommandRunner
{
    private readonly ILogger _logger;

    public CommandRunner(ILogger logger)
    {
        _logger = logger;
    }

    public Result<string?> Run(string command, string arguments)
    {
        try
        {
            using var process = new Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error running command {Command} {Arguments}", command, arguments);
            return Result.Failure<string>(CommandRunnerError.RunCommandError(e.Message));
        }
    }
}