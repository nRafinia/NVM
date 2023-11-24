using Creator.Docker.Command.Linux.Shared;
using Microsoft.Extensions.Logging;
using Moq;

namespace Creator.Docker.Command.Linux.Test.Shared;

public class CommandRunnerTests
{
    private readonly CommandRunner _commandRunner;

    public CommandRunnerTests()
    {
        var mockLogger = new Mock<ILogger>();
        _commandRunner = new CommandRunner(mockLogger.Object);
    }

    [Fact]
    public void Run_ShouldReturnFailure_WhenCommandDoesNotExist()
    {
        var result = _commandRunner.Run("nonexistentcommand", "");

        Assert.False(result.IsSuccess);
        Assert.Equal("CommandRunner.Error", result.Error?.Code);
    }


}