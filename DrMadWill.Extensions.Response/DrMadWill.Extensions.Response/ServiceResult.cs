namespace DrMadWill.Extensions.Response;

public class ServiceResult
{
    public bool Success { get; }
    public bool Fail => !Success;
    public string? Message { get; set; }
    public string[]? Errors { get; set; }
    public ServiceResult(bool status, string? message )
    {
        Success = status;
        Message = message;
    }

    public ServiceResult()
    {
    }
    
    public ServiceResult(bool status, string? message, string[]? errors)
    {
        Success = status;
        Message = message;
        Errors = errors;
    }

    public static ServiceResult Failed(string? message = null, string[]? errors = null) =>
        new ServiceResult(false, message, errors);
    
    public static ServiceResult Succeed( string? message = null)
         => new ServiceResult(true, message);
    
}