using System.Diagnostics;

namespace Creator.Shared;

public delegate void WriteInputEvent(string command);

public class RunCommand : IDisposable
{
    private Process? _process;
    
    public WriteInputEvent RunProcess(string path, string args, Action<string>? onStdOut = null,
        Action<string>? onStdErr = null)
    {
        var readStdOut = onStdOut != null;
        var readStdErr = onStdErr != null;
        
        _process = new Process
        {
            StartInfo =
            {
                FileName = path,
                Arguments = args,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = readStdOut,
                RedirectStandardError = readStdErr,
                RedirectStandardInput = true,
                
            }
        };

        _process.Start();

        if (readStdOut) Task.Run(() => ReadStream(_process.StandardOutput, onStdOut));
        if (readStdErr) Task.Run(() => ReadStream(_process.StandardError, onStdErr));

        //_process.WaitForExit();

        //return process.ExitCode;

        
        return WriteToInput;
    }

    private static void ReadStream(TextReader textReader, Action<string>? callback)
    {
        while (true)
        {
            var line = textReader.ReadLine();
            if (line == null)
            {
                break;
            }

            callback?.Invoke(line);
        }
    }

    private void WriteToInput(string command)
    {
        var streamWriter = _process?.StandardInput;
        streamWriter?.WriteLine(command);
    }

    public bool IsExited => _process?.HasExited ?? true;
    
    public void Dispose()
    {
        //_process.WaitForExit();
        //_process.ExitCode;
        _process?.Dispose();
        //GC.
    }
}