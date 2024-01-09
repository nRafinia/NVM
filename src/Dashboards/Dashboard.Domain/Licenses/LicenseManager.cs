using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Dashboard.Domain.Constants;
using Dashboard.Domain.Errors;
using SharedKernel.Base.Results;

namespace Dashboard.Domain.Licenses;

public static class LicenseManager
{
    private static License? _license;

    private const string LicensePath = "license";

    public static async Task<Result<License?>> Load()
    {
        if (_license is not null)
        {
            return _license;
        }

        var licenseResult = await LoadFromDisk();
        if (licenseResult.IsFailure)
        {
            return licenseResult;
        }

        var validateLicenseResponse = IsValidLicense(licenseResult.Value);
        if (validateLicenseResponse.IsFailure)
        {
            return Result.Failure<License?>(validateLicenseResponse.Error!);
        }

        _license = licenseResult.Value!;

        return _license;
    }

    public static bool IsValid()
    {
        return IsValidLicense(_license).IsSuccess;
    }

    #region Get methods

    public static Result<License?> Get()
    {
        var validateLicenseResponse = IsValidLicense(_license);
        return validateLicenseResponse.IsFailure 
            ? Result.Failure<License?>(validateLicenseResponse.Error!) 
            : _license;
    }

    public static Result<LicenseType?> GetLicenseType()
    {
        var validateLicenseResponse = IsValidLicense(_license);
        return validateLicenseResponse.IsFailure 
            ? Result.Failure<LicenseType?>(validateLicenseResponse.Error!) 
            : _license!.Type;
    }

    public static Result<DateOnly?> GetExpireDate()
    {
        return _license?.ExpirationDate ?? Result.Failure<DateOnly?>(LicenseError.LicenseIsInvalid);
    }

    public static Result<string?> GetCompany()
    {
        var validateLicenseResponse = IsValidLicense(_license);
        return validateLicenseResponse.IsFailure 
            ? Result.Failure<string?>(validateLicenseResponse.Error!) 
            : _license!.Company;
    }

    public static Result<string?> GetName()
    {
        var validateLicenseResponse = IsValidLicense(_license);
        return validateLicenseResponse.IsFailure 
            ? Result.Failure<string?>(validateLicenseResponse.Error!) 
            : _license!.Name;
    }

    public static Result<string?> GetEmail()
    {
        var validateLicenseResponse = IsValidLicense(_license);
        return validateLicenseResponse.IsFailure 
            ? Result.Failure<string?>(validateLicenseResponse.Error!) 
            : _license!.Email;
    }

    public static Result<byte[]?> GetKey()
    {
        var validateLicenseResponse = IsValidLicense(_license);
        return validateLicenseResponse.IsFailure 
            ? Result.Failure<byte[]?>(validateLicenseResponse.Error!) 
            : Convert.FromBase64String(_license!.Key);
    }

    #endregion

    #region private methods

    private static Result<bool?> IsValidLicense(License? license)
    {
        if (license is null)
        {
            return Result.Failure<bool?>(LicenseError.LicenseIsInvalid);
        }

        if (license.ExpirationDate < DateOnly.FromDateTime(DateTime.Now))
        {
            return Result.Failure<bool?>(LicenseError.LicenseIsExpired(license.ExpirationDate));
        }

        return true;
    }

    private static async Task<Result<License?>> LoadFromDisk()
    {
        if (!File.Exists(LicensePath))
        {
            return Result.Failure<License>(LicenseError.LicenseNotFound);
        }

        var licenseContent = await File.ReadAllTextAsync(LicensePath);
        var licenseWithSign = JsonSerializer.Deserialize<LicenseSign>(licenseContent);

        if (licenseWithSign is null)
        {
            return Result.Failure<License>(LicenseError.LicenseHasProblem);
        }

        var licenseData = new License()
        {
            Company = licenseWithSign.Company,
            Name = licenseWithSign.Name,
            Email = licenseWithSign.Email,
            Type = licenseWithSign.Type,
            Key = licenseWithSign.Key,
            ExpirationDate = licenseWithSign.ExpirationDate
        };

        if (!VerifyLicense(licenseData, licenseWithSign.Signature))
        {
            return Result.Failure<License>(LicenseError.LicenseIsInvalid);
        }

        _license = licenseData;

        return _license;
    }

    private static bool VerifyLicense(License license, string signature)
    {
        var publicKey = Convert.FromBase64String(LicenseConst.PublicKey);
        return Verify(license, signature, publicKey);
    }

    private static bool Verify(License data, string sign, byte[] publicKey)
    {
        var dataString = JsonSerializer.Serialize(data);
        var signForCheck = Convert.FromBase64String(sign);

        var verifyProvider = new RSACryptoServiceProvider();
        verifyProvider.ImportRSAPublicKey(publicKey, out _);

        return verifyProvider.VerifyData(
            Encoding.UTF8.GetBytes(dataString),
            signForCheck,
            HashAlgorithmName.SHA512,
            RSASignaturePadding.Pkcs1);
    }

    #endregion
}