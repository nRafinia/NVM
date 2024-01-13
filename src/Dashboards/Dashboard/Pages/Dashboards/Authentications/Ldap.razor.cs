using Authorizer.Common.Models;
using Authorizer.Local.Application.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Dashboard.Application.LDAPs.Queries.GetAllLDAPs;
using Dashboard.Application.LDAPs.Queries.GetLDAPByName;
using Dashboard.Application.Users.Models;
using Dashboard.Const;

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
}