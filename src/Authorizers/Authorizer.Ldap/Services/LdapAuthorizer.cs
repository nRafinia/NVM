using System.DirectoryServices.Protocols;
using System.Text;
using Authorizer.Common.Abstractions;
using Authorizer.Common.Models;
using Authorizer.Ldap.Helpers;
using Authorizer.Ldap.Models;
using Mapster;
using SharedKernel.Base.Results;
using SharedKernel.Errors;

namespace Authorizer.Ldap.Services;

public class LdapAuthorizer(LdapConfiguration configuration) : IAuthorizer
{
    public Result<List<UserInfo>?> GetUsers()
    {
        var attributesToQuery = new[]
        {
            configuration.Attributes.UniqueId,
            configuration.Attributes.UserName,
            configuration.Attributes.DisplayName
        };

        SearchResponse searchResults;
        try
        {
            searchResults = LdapTools.Search(configuration, attributesToQuery);
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
        userConfiguration.Username = userName;
        userConfiguration.Password = password;
        userConfiguration.FilterQuery = $"(&(objectCategory=Person)(sAMAccountName={userName}))";

        try
        {
            var searchResults = LdapTools.Search(userConfiguration, attributesToQuery);
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