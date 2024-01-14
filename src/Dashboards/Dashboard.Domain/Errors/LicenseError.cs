using SharedKernel.Base;

namespace Dashboard.Domain.Errors;

public static class LicenseError
{
    public static readonly Error LicenseNotFound = new("License.NotFound",
        "The license file could not be found in the expected location. Please verify that the license file exists and is accessible.");

    public static readonly Error LicenseHasProblem = new("License.HasProblem",
        "An unexpected problem has occurred with the license file. It may be corrupted, or there may be a problem with the file access permissions.");

    public static readonly Error LicenseIsInvalid = new("License.Invalid",
        "The license provided is not valid. Please verify the contents of your license file and ensure it has been correctly generated.");

    public static Error LicenseIsExpired(DateOnly expireDate) => new("License.Expired",
        $"The license has expired as of {expireDate}. Please renew your license to continue using the software.");
}