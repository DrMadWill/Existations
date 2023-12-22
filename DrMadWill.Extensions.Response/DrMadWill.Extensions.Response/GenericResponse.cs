using Newtonsoft.Json;

namespace DrMadWill.Extensions.Response;

/// <summary>
/// Result response class
/// Geriyə statik responsu qaytarmaq üçün istifadə olunur.
/// </summary>
public class GenericResponse<Type>
{
    public static string Success = "Success";
    [JsonConstructor]
    public GenericResponse(bool status, int genericStatus, string? message)
    {
        GenericStatus = genericStatus;
        Message = message;
        Status = status;
    }

    [JsonConstructor]
    public GenericResponse(bool status, int genericStatus, Type data, string? message)
    {
        GenericStatus = genericStatus;
        Message = message;
        Data = data;
        Status = status;
    }

    public GenericResponse()
    {

    }

    [JsonProperty]
    public int GenericStatus { get; set; }
    [JsonProperty]
    public bool Status { get; set; }
    [JsonProperty]
    public string? Message { get; set; }
    [JsonProperty]
    public Type? Data { get; set; }

    [JsonProperty]
    public string[]? Error { get; set; }

    public static GenericResponse<Type> Succeed(Type data, string msg = "Success")
    {
        return new GenericResponse<Type>(true, 200, data, msg);
    }

    public static GenericResponse<Type> Failed(string msg, string[]? err = null, int specialStatusCode = 400)
    {
        return new GenericResponse<Type>
        {
            Error = err,
            Message = msg,
            Status = false,
            GenericStatus = specialStatusCode

        };
    }

}

