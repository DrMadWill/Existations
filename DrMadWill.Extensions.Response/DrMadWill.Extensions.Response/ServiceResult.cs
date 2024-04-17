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

    public static ServiceResult Failed(string? message = null, string[]? errors = null) => new(false, message, errors);
    
    public static ServiceResult Succeed(string? message = null) => new(true, message);

    public GenericServiceResult<T> GenerateGenericServiceResult<T>(T result)
        => new(result, Success, Message, Errors);
}

public class GenericServiceResult<T>
{
    public T Result { get; set; }
    public bool Success { get; }
    public bool Fail => !Success;
    public bool ResultNotNull => Result != null;
    public string? Message { get; set; }
    public string[]? Errors { get; set; }
    
    public GenericServiceResult(T result,bool status, string? message)
    {
        Result = result;
        Success = status;
        Message = message;
    }
    
    public GenericServiceResult()
    {
    }
    
    public GenericServiceResult(T result,bool status, string? message, string[]? errors)
    {
        Result = result;
        Success = status;
        Message = message;
        Errors = errors;
    }

    public static GenericServiceResult<T> Failed(T result, string? message = null, string[]? errors = null) =>
        new(result, false, message, errors);
    
    public static GenericServiceResult<T> Succeed(T result, string? message = null) => new(result, true, message);
    
    
    public ServiceResult GetServiceResult() =>
        new(Success, Message, Errors);
    
    public static ServiceResult GetServiceResult(GenericServiceResult<T> result)
        => new(result.Success,result.Message,result.Errors);

}