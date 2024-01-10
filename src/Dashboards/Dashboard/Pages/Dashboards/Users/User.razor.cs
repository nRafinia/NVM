using Authorizer.Common.Models;
using Authorizer.Ldap.Services;
using Authorizer.Local.Application.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Dashboard.Application.Credentials.Queries.GetAllCredentials;
using Dashboard.Application.Credentials.Queries.GetCredentialByName;
using Dashboard.Application.Users.Models;
using Dashboard.Const;

namespace Dashboard.Pages.Dashboards.Users;

public partial class User : DashboardPage
{
    public readonly string PageTitle = "User";
    public override Guid Id => DashboardIds.User;

    private async Task LoadData()
    {
        _allUsers = _authorizerType switch
        {
            AuthorizerType.Local => await LoadLocalUsers(),
            AuthorizerType.LDAP => await LoadLdapUsers(),
            _ => throw new ArgumentOutOfRangeException()
        };

        _users = _allUsers;
        StateHasChanged();
    }

    private void FilterSearchText()
    {
        _users = _allUsers.Where(x => x.UserName.Contains(SearchText!, StringComparison.OrdinalIgnoreCase)).ToList();
        StateHasChanged();
    }

    private async Task<List<UserInfo>> LoadLocalUsers()
    {
        var authorizer = new LocalAuthorizer(ServiceProvider);
        var getUserResponse = await authorizer.GetUsers();
        if (getUserResponse.IsFailure)
        {
            await Swal.FireAsync("Error", getUserResponse.Error!.Message, SweetAlertIcon.Error);
            return new List<UserInfo>(0);
        }

        return getUserResponse.Value!;
    }

    private async Task<List<UserInfo>> LoadLdapUsers()
    {
        //var authorizer = new LdapAuthorizer();
        return new List<UserInfo>(0);
    }
}