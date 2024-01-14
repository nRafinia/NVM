using Authorizer.Common.Models;
using CurrieTechnologies.Razor.SweetAlert2;
using Dashboard.Application.Credentials.Commands.CommitCredentialsChanges;
using Dashboard.Application.Credentials.Commands.DeleteCredentials;
using Dashboard.Application.LDAPs.Queries.GetAllLDAPs;
using Dashboard.Application.LDAPs.Queries.GetLDAPByName;
using Dashboard.Application.Users.Models;
using Dashboard.Application.Users.Queries.GetDirectoryHasUser;
using Dashboard.Const;
using SharedKernel.Base.Results;
using SharedKernel.Errors;
using SharedKernel.ValueObjects;
using AuthorizerType = Dashboard.Domain.Entities.Users.Enums.AuthorizerType;

namespace Dashboard.Pages.Dashboards.Authentications;

public partial class Ldap : DashboardPage
{
    public readonly string PageTitle = "LDAP";
    public override Guid Id => DashboardIds.LDAP;

    private async Task LoadData()
    {
        var ldapResponse = string.IsNullOrWhiteSpace(SearchText)
            ? await Mediator.Send(new GetAllLdapsQuery())
            : await Mediator.Send(new GetLDAPByNameQuery(SearchText!));
        if (ldapResponse.IsFailure)
        {
            await Swal.FireAsync("Error", ldapResponse.Error!.Message, SweetAlertIcon.Error);
            return;
        }

        _ldaps = ldapResponse.Value!.ToList();
        StateHasChanged();
    }

    private async Task Delete(IdColumn id)
    {
        var ldap = _ldaps.FirstOrDefault(x => x.Id == id);
        if (ldap is null)
        {
            return;
        }

        var directoryHasChildren = await CheckLdapHasChildren(id);
        if (directoryHasChildren.IsFailure)
        {
            return;
        }
        
        var deleteConfirmation = await Swal.FireAsync(new SweetAlertOptions("Delete")
        {
            Text = $"Are you sure for delete '{ldap.Name}' LDAP?",
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

    private async Task<Result> CheckLdapHasChildren(IdColumn id)
    {
        var existResponse = await Mediator.Send(new GetDirectoryHasUserQuery(id, AuthorizerType.LDAP));
        if (existResponse.IsFailure)
        {
            await Swal.FireAsync("Error", existResponse.Error!.Message, SweetAlertIcon.Error);
            return Result.Failure(existResponse.Error!);
        }

        var existUser= existResponse.Value;
        if (!existUser)
        {
            return Result.Success();
        }
        
        await Swal.FireAsync("Error", "LDAP has users and cannot be deleted", SweetAlertIcon.Error);
        return Result.Failure(SharedErrors.AccessDenied);
    }
}