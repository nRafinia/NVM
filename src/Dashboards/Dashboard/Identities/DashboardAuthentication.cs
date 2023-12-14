using System.Security.Claims;
using System.Text.Json;
using Dashboard.Application.Users;
using Dashboard.Application.Users.Models;
using Dashboard.Domain.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Dashboard.Identities;

public class DashboardAuthentication : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _protectedLocalStorage;
    private readonly IUserLogic _userLogic;
    private readonly ILogger<DashboardAuthentication> _logger;

    private User? User { get; set; }

    public DashboardAuthentication(ProtectedLocalStorage protectedLocalStorage, ILogger<DashboardAuthentication> logger,
        IUserLogic userLogic)
    {
        _protectedLocalStorage = protectedLocalStorage;
        _logger = logger;
        _userLogic = userLogic;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var user = await GetUserSession();

            return user is not null
                ? await GenerateAuthenticationState(user)
                : await GenerateEmptyAuthenticationState();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting authentication state, error={Message}", ex.Message);
            await LogoutAsync();
            return default!;
        }
    }

    public async Task<bool> LoginAsync(LoginFormModel loginFormModel)
    {
        var (user, isSuccess) = await AuthenticateUser(loginFormModel.UserName, loginFormModel.Password);

        if (isSuccess)
        {
            await SetUserSession(user!);
            NotifyAuthenticationStateChanged(GenerateAuthenticationState(user!));
        }

        return isSuccess;
    }

    public async Task LogoutAsync()
    {
        RefreshUserSession(null);
        await _protectedLocalStorage.DeleteAsync(IdentityConst.StorageKey);
        NotifyAuthenticationStateChanged(GenerateEmptyAuthenticationState());
    }

    #region private methods
    
    private Task<AuthenticationState> GenerateAuthenticationState(User user)
    {
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.GivenName, user.Name)
        }, IdentityConst.AuthenticationType);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    private async Task<(User?, bool)> AuthenticateUser(string userName, string password)
    {
        var userResult = await _userLogic.AuthenticateAsync(new LoginRequest(userName, password));

        return (userResult, userResult is not null);
    }

    private async Task<(User?, bool)> LookUpUser(int userId, string userName)
    {
        var userResult = await _userLogic.GetProfileAsync(new GetProfileRequest(userId, userName));

        return (userResult, userResult is not null);
    }

    private async Task<User?> GetUserSession()
    {
        if (User is not null)
        {
            return User;
        }

        try
        {
            var storedPrincipal = await _protectedLocalStorage.GetAsync<string>(IdentityConst.StorageKey);

            if (string.IsNullOrWhiteSpace(storedPrincipal.Value) || !storedPrincipal.Success)
            {
                return null;
            }

            var userSessionInfo = JsonSerializer.Deserialize<UserSession>(storedPrincipal.Value);
            if (userSessionInfo is null)
            {
                return null;
            }

            var (user, isLookUpSuccess) = await LookUpUser(userSessionInfo.UserId, userSessionInfo.UserName);

            return isLookUpSuccess
                ? RefreshUserSession(user!)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting authentication state in GetUserSession, error={Message}",
                ex.Message);
            await LogoutAsync();
            return null;
        }
    }
    
    private ValueTask SetUserSession(User user)
    {
        RefreshUserSession(user);

        return _protectedLocalStorage.SetAsync(IdentityConst.StorageKey, JsonSerializer.Serialize(user));
    }

    private User? RefreshUserSession(User? user) => User = user;

    private Task<AuthenticationState> GenerateEmptyAuthenticationState() =>
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
    
    #endregion
}