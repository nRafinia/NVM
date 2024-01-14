namespace Dashboard.Domain.Entities.LDAPs.Enums;

public enum AuthType
{
    /// <summary>Indicates that the connection should be made without passing credentials. The value is equal to 0.</summary>
    Anonymous = 0,

    /// <summary>Indicates that basic authentication should be used on the connection. The value is equal to 1.</summary>
    Basic = 1,

    /// <summary>Indicates that Microsoft Negotiate authentication should be used on the connection. The value is equal to 2.</summary>
    Negotiate = 2,

    /// <summary>Indicates that Windows NT Challenge/Response (NTLM) authentication should be used on the connection. The value is equal to 3.</summary>
    Ntlm = 3,

    /// <summary>Indicates that the Digest Access Authentication should be used on the connection. The value is equal to 4.</summary>
    Digest = 4,

    /// <summary>Indicates a negotiation mechanism (Sicily) will be used to choose MSN, DPA or NTLM.  This should be used for LDAPv2 servers only. The value is equal to 5.</summary>
    Sicily = 5,

    /// <summary>Indicates that Distributed Password Authentication (DPA) should be used on the connection. The value is equal to 6.</summary>
    Dpa = 6,

    /// <summary>Indicates that it is authenticated by "Microsoft Network Authentication Service". The value is equal to 7.</summary>
    Msn = 7,

    /// <summary>Indicates an external method will be used to authenticate the connection. The value is equal to 8.</summary>
    External = 8,

    /// <summary>Indicates that Kerberos authentication should be used on the connection. The value is equal to 9.</summary>
    Kerberos = 9,
}