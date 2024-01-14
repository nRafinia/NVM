using System.Text.Json;
using JetBrains.Annotations;
using SharedKernel.Entities;
using SharedKernel.Enums;

namespace Dashboard.Domain.Test.Entities;

[TestSubject(typeof(Credential))]
public class CredentialTest
{
    [Fact]
    public void TestUpdateName_ShouldSetNewNameAndDescription()
    {
        // Arrange
        var credential = Credential.None("OldName");

        // Act
        credential.UpdateName("NewName", "NewDescription");

        // Assert
        Assert.Equal("NewName", credential.Name);
        Assert.Equal("NewDescription", credential.Description);
    }    
    
    [Fact]
    public void TestJson_ShouldBeDeserialize_None()
    {
        // Arrange
        var credential = Credential.None("Name");
        var credentialJson = JsonSerializer.Serialize(credential);

        // Act
        var newCredential = JsonSerializer.Deserialize<Credential>(credentialJson);

        // Assert
        Assert.NotNull(newCredential);
        Assert.Equal("Name", newCredential.Name);
    }

    [Fact]
    public void TestNone_ShouldReturnCredentialWithNoneType()
    {
        // Arrange
        string name = "CredentialName";
        string description = "CredentialDescription";

        // Act
        var credential = Credential.None(name, description);

        // Assert
        Assert.Equal(name, credential.Name);
        Assert.Equal(description, credential.Description);
        Assert.Equal(CredentialType.None, credential.CredentialType);
        Assert.Null(credential.BasicCredential);
    }

    [Fact]
    public void TestBasic_ShouldReturnCredentialWithBasicTypeAndSetUserNameAndPassword()
    {
        // Arrange
        string name = "CredentialName";
        string description = "CredentialDescription";
        string username = "Username";
        string password = "Password";

        // Act
        var credential = Credential.Basic(name, username, password, description);

        // Assert
        Assert.Equal(name, credential.Name);
        Assert.Equal(description, credential.Description);
        Assert.Equal(CredentialType.Basic, credential.CredentialType);
        Assert.NotNull(credential.BasicCredential);
        Assert.Equal(username, credential.BasicCredential.UserName);
        Assert.Equal(password, credential.BasicCredential.Password);
    }
    
    [Fact]
    public void TestJson_ShouldBeDeserialize_Basic()
    {
        // Arrange
        string name = "CredentialName";
        string description = "CredentialDescription";
        string username = "Username";
        string password = "Password";
        var credential = Credential.Basic(name, username, password, description);
        var credentialJson = JsonSerializer.Serialize(credential);

        // Act
        var newCredential = JsonSerializer.Deserialize<Credential>(credentialJson);

        // Assert
        Assert.NotNull(newCredential);
        Assert.Equal(name, newCredential.Name);
        Assert.Equal(CredentialType.Basic, newCredential.CredentialType);
        Assert.NotNull(newCredential.BasicCredential);
        Assert.Equal(username, newCredential.BasicCredential.UserName);
        Assert.Equal(password, newCredential.BasicCredential.Password);
    }

    [Fact]
    public void TestAddBasic_ShouldSetCredentialTypeToBasicAndSetUserNameAndPassword()
    {
        // Arrange
        string name = "CredentialName";
        string username = "Username";
        string password = "Password";
        var credential = Credential.None(name);

        // Act
        var basicCredential = credential.AddBasic(username, password);

        // Assert
        Assert.Equal(CredentialType.Basic, credential.CredentialType);
        Assert.Equal(basicCredential, credential.BasicCredential);
        Assert.Equal(username, basicCredential.UserName);
        Assert.Equal(password, basicCredential.Password);
    }
    
}