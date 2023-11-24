using Shared.Domain.Base;

namespace Creator.Docker.Command.Linux.Errors;

internal static class CommandRunnerError
{
    public static Error RunCommandError(string message) => new("CommandRunner.Error", message);
}