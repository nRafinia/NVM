using System.DirectoryServices.Protocols;
using Authorizer.Common.Abstractions;
using Authorizer.Common.Models;
using Authorizer.Ldap.Helpers;
using Authorizer.Ldap.Models;
using Mapster;
using SharedKernel.Base.Results;
using SharedKernel.Entities;
using SharedKernel.Enums;
using SharedKernel.Errors;

namespace Authorizer.Ldap.Services;

public class LdapAuthorizer(LdapConfiguration configuration) : IAuthorizer
{
    /// <summary>
    /// Retrieves a list of user information based on the provided credential.
    /// </summary>
    /// <param name="credential">
    /// The credential used for authentication and authorization.
    /// For Microsoft Active Directory, provide the username.
    /// For other systems, provide the domain\username.
    /// Example: user@domain.name or cn=user,dc=domain,dc=name
    /// </param>
    /// <returns>
    /// Returns a list of user information if successful, otherwise returns a failure result with an error message.
    /// </returns>
    public Result<List<UserInfo>?> GetUsers(Credential credential)
    {
        if (credential.CredentialType == CredentialType.None)
        {
            return Result.Failure<List<UserInfo>>(SharedErrors.InvalidCredentialType);
        }

        var attributesToQuery = new[]
        {
            configuration.Attributes.UniqueId,
            configuration.Attributes.UserName,
            configuration.Attributes.DisplayName
        };

        SearchResponse searchResults;
        try
        {
            searchResults = LdapTools.Search(
                configuration, 
                credential.BasicCredential!.UserName, 
                credential.BasicCredential!.Password,
                attributesToQuery);
        }
        catch (Exception e)
        {
            return Result.Failure<List<UserInfo>>(SharedErrors.InternalErrorMessage(e.Message));
        }

        var searchResult = searchResults.Entries.Cast<SearchResultEntry>().ToList();
        var result = searchResult.Select(CastResultToUserInfo).ToList();

        return result;
    }

    public Result<UserInfo?> SignIn(string userName, string password)
    {
        var attributesToQuery = new[]
        {
            configuration.Attributes.UniqueId
        };

        var userConfiguration = configuration.Adapt<LdapConfiguration>();
        userConfiguration.FilterQuery = $"(&(objectCategory=Person)(sAMAccountName={userName}))";

        try
        {
            var searchResults = LdapTools.Search(
                userConfiguration, 
                userName,
                password,
                attributesToQuery);
            return searchResults.Entries.Count > 0
                ? CastResultToUserInfo(searchResults.Entries[0])
                : Result.Failure<UserInfo>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            return Result.Failure<UserInfo>(SharedErrors.InternalErrorMessage(e.Message));
        }
    }

    #region private methods

    private UserInfo CastResultToUserInfo(SearchResultEntry searchEntry)
    {
        var uniqueId = searchEntry.Attributes.Contains(configuration.Attributes.UniqueId)
            ? GetAttributeValue(searchEntry.Attributes[configuration.Attributes.UniqueId])
            : string.Empty;
        var userName = searchEntry.Attributes.Contains(configuration.Attributes.UserName)
            ? GetAttributeValue(searchEntry.Attributes[configuration.Attributes.UserName])
            : string.Empty;
        var displayName = searchEntry.Attributes.Contains(configuration.Attributes.DisplayName)
            ? GetAttributeValue(searchEntry.Attributes[configuration.Attributes.DisplayName])
            : string.Empty;

        return new UserInfo(uniqueId, displayName, userName);
    }

    private static string GetAttributeValue(DirectoryAttribute attributes)
    {
        if (attributes[0] is byte[] attrValue)
        {
            return string.Concat(attrValue.Select(b => b.ToString("X2")));
        }

        return attributes[0]?.ToString() ?? string.Empty;
    }

    #endregion
}