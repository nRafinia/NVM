using Agent.UI.Application.Abstractions.Models.FileManager;
using Microsoft.AspNetCore.Components;

namespace Agent.UI.Pages.FileManagers;

public partial class FileManager
{
    private IList<FileListItem>? _pathItems;
    private string? _path;
    private OperationSystemType _systemType;
    private IList<string> _logicalDrives = new List<string>(0);
    private string _currentRoot = "/";
    private char _pathSeparator = '/';

    private JavaScriptCallback<string>? _scriptCallback;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        await SetOsInformation();

        _currentRoot = _logicalDrives.First();
        _pathSeparator = _systemType == OperationSystemType.Windows ? '\\' : '/';

        await LoadPath(_currentRoot, string.Empty);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JavaScript.Load("/js/file-manager.js");
        _scriptCallback = new JavaScriptCallback<string>(JsRuntime);
        await _scriptCallback.Set(ExternalCreateFolder);
    }
    
    private async Task SetOsInformation()
    {
        var osInformationResponse = await HardwareInformation.GetOs();
        if (osInformationResponse.IsFailure)
        {
            return;
        }

        _systemType = osInformationResponse.Value!.Type;
        _logicalDrives = osInformationResponse.Value!.LogicalDrives;
    }

    private async Task ChangePath(string folderName)
    {
        var pathBegin = _path!.Remove(0, _currentRoot.Length);

        if (!string.IsNullOrWhiteSpace(pathBegin))
        {
            pathBegin += "\\";
        }

        await LoadPath(_currentRoot, pathBegin + folderName);
    }

    private async Task GoUp()
    {
        var path = _path!
            .Remove(0, _currentRoot.Length)
            .Split(_pathSeparator);

        if (path.Length == 1)
        {
            await LoadPath(_currentRoot, string.Empty);
        }

        var targetPath = string.Join(_pathSeparator, path[..^1]);

        await LoadPath(_currentRoot, targetPath);
    }

    private Task RefreshCurrentFolder()
    {
        return LoadPath(_currentRoot, string.Empty);
    }

    private async Task LoadPath(string root, string? path)
    {
        var getPathResponse = await FileManagerLogic.GetPath(new GetPathRequest(root, path));
        if (getPathResponse.IsFailure)
        {
            return;
        }

        _path = getPathResponse.Value!.Path;
        _pathItems = getPathResponse.Value!.Items;
    }

    private Task ChangeDrive(ChangeEventArgs e)
    {
        var selectedDrive = e.Value?.ToString();
        if (string.IsNullOrWhiteSpace(selectedDrive))
        {
            return Task.CompletedTask;
        }
        _currentRoot = selectedDrive;
        return LoadPath(_currentRoot, string.Empty);
    }

    #region Folder

    private async Task DeleteFolder(string folderName)
    {
        var confirmDelete = await Swal.FireAsync(new SweetAlertOptions()
        {
            Title = "Delete",
            Text = $"Are you sure for delete folder '{folderName}'?",
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            ConfirmButtonText = "Yes"
        });

        if (!confirmDelete.IsConfirmed)
        {
            return;
        }

        var pathBegin = _path!.Remove(0, _currentRoot.Length);

        if (!string.IsNullOrWhiteSpace(pathBegin))
        {
            pathBegin += "\\";
        }

        var deleteFolderResponse = await FileManagerLogic.DeleteFolder(new DeleteFolderRequest(_currentRoot, pathBegin + folderName));
        if (deleteFolderResponse.IsSuccess)
        {
            await Swal.FireAsync(null, $"Folder '{folderName}' removed.", SweetAlertIcon.Success);
            await RefreshCurrentFolder();
            return;
        }

        await Swal.FireAsync("Error", deleteFolderResponse.Error!.Message, SweetAlertIcon.Error);
    }

    private async Task CreateFolder(string folderName)
    {
        var pathBegin = _path!.Remove(0, _currentRoot.Length);

        if (!string.IsNullOrWhiteSpace(pathBegin))
        {
            pathBegin += "\\";
        }

        await FileManagerLogic.CreateFolder(new CreateFolderRequest(_currentRoot, pathBegin + folderName));
    }

    private async Task ExternalCreateFolder(string folderName)
    {
        Logger.LogInformation("----->"+folderName);
    }

    #endregion
}