using CurrieTechnologies.Razor.SweetAlert2;
using Dashboard.Application.Credentials.Commands.CommitCredentialsChanges;
using Dashboard.Application.Credentials.Commands.DeleteCredentials;
using Dashboard.Application.Credentials.Queries.GetAllCredentials;
using Dashboard.Application.Credentials.Queries.GetCredentialByName;
using Dashboard.Const;
using SharedKernel.ValueObjects;

namespace Dashboard.Pages.Dashboards.Credentials;

public partial class Credential : DashboardPage
{
    public readonly string PageTitle = "Credential";
    public override Guid Id => DashboardIds.Credential;
    
    private async Task LoadData()
    {
        var credentialsResponse = string.IsNullOrWhiteSpace(SearchText)
            ? await Mediator.Send(new GetAllCredentialQuery())
            : await Mediator.Send(new GetCredentialByNameQuery(SearchText!));
        if (credentialsResponse.IsFailure)
        {
            await Swal.FireAsync("Error", credentialsResponse.Error!.Message, SweetAlertIcon.Error);
            return;
        }

        _credentials = credentialsResponse.Value!.ToList();
        StateHasChanged();
    }
    
    private async Task Delete(IdColumn id)
    {
        var credential = _credentials.FirstOrDefault(x => x.Id == id);
        if (credential is null)
        {
            return;
        }

        var deleteConfirmation = await Swal.FireAsync(new SweetAlertOptions("Delete")
        {
            Text = $"Are you sure for delete '{credential.Name}' credential?",
            Icon = SweetAlertIcon.Warning,
            ShowCancelButton = true,
            ConfirmButtonText = "Yes, delete it!",
            CancelButtonText = "No, keep it"
        });

        if (deleteConfirmation.IsDismissed)
        {
            return;
        }

        var deleteResponse = await Mediator.Send(new DeleteCredentialCommand(id));
        if (deleteResponse.IsFailure)
        {
            await Swal.FireAsync("Error", deleteResponse.Error!.Message, SweetAlertIcon.Error);
            return;
        }

        await Mediator.Send(new CommitCredentialChanges());

        await LoadData();
    }
}