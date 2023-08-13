using FileManager.Application.Abstraction;

namespace FileManager.Infra.Services;

public class FileService : IFiles
{
    private const string RootPath = @"d:\";

    
    
    public void GetFiles(string path)
    {
        var directories = Directory.GetDirectories(RootPath);
        var files = Directory.GetFiles(RootPath);
        
        
    }
}