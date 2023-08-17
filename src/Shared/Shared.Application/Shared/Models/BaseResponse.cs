namespace Shared.Application.Shared.Models;

public class BaseResponse
{
    public ResponseStatusType Status { get; set; } = ResponseStatusType.Failed;
    public string StatusCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public bool IsOk => Status == ResponseStatusType.Ok;
    public bool IsFailed => Status == ResponseStatusType.Failed;
}

public class BaseResponse<T> : BaseResponse
{
    public required T Data { get; set; }
}