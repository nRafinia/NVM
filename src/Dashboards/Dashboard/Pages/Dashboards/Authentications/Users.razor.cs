using Authorizer.Common.Models;
using CurrieTechnologies.Razor.SweetAlert2;
using Dashboard.Application.LDAPs.Queries.GetAllLDAPs;
using Dashboard.Application.Users.Models;
using Dashboard.Application.Users.Queries.GetAllUsers;
using Dashboard.Application.Users.Queries.GetLdapUsers;
using Dashboard.Application.Users.Queries.GetLdapUsersByName;
using Dashboard.Application.Users.Queries.GetLocalUsers;
using Dashboard.Application.Users.Queries.GetLocalUsersByName;
using Dashboard.Application.Users.Queries.GetUserByUserName;
using Dashboard.Const;
using Dashboard.Domain.Entities.Users;

namespace Dashboard.Pages.Dashboards.Authentications;

public partial class Users : DashboardPage
{
    public readonly string PageTitle = "User";
    public override Guid Id => DashboardIds.User;

    private async Task LoadData()
    {
        _users = _authorizerType switch
        {
            AuthorizerType.Local => await LoadLocalUsers(),
            AuthorizerType.LDAP => await LoadLdapUsers(),
            _ => throw new ArgumentOutOfRangeException()
        };

        StateHasChanged();
    }

    private async Task<List<User>> LoadLocalUsers()
    {
        var getUserResponse = string.IsNullOrWhiteSpace(SearchText)
            ? await Mediator.Send(new GetLocalUsersQuery())
            : await Mediator.Send(new GetLocalUsersByNameQuery(SearchText!));
        if (getUserResponse.IsFailure)
        {
            await Swal.FireAsync("Error", getUserResponse.Error!.Message, SweetAlertIcon.Error);
            return new List<User>(0);
        }

        return getUserResponse.Value!;
    }

    private async Task<List<User>> LoadLdapUsers()
    {
        var getUserResponse = string.IsNullOrWhiteSpace(SearchText)
            ? await Mediator.Send(new GetLdapUsersQuery(_ldapId!))
            : await Mediator.Send(new GetLdapUsersByNameQuery(_ldapId!, SearchText!));
        if (getUserResponse.IsFailure)
        {
            await Swal.FireAsync("Error", getUserResponse.Error!.Message, SweetAlertIcon.Error);
            return new List<User>(0);
        }

        return getUserResponse.Value!;
    }

    private async Task GetLdaps()
    {
        var ldapsResponse = await Mediator.Send(new GetAllLdapsQuery());
        if (ldapsResponse.IsFailure)
        {
            return;
        }
        
        _ldaps = ldapsResponse.Value!;
    }
}