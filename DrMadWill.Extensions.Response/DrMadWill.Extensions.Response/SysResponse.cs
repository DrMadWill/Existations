using Newtonsoft.Json;

namespace DrMadWill.Extensions.Response;

/// <summary>
/// Result response class
/// Geriyə statik respons qaytarmaq üçün istifadə olunur.
/// </summary>
public class SysResponse
{
    public static string Success = "Success";
    [JsonConstructor]
    public SysResponse(bool status, int genericStatus, string? message)
    {
        GenericStatus = genericStatus;
        Message = message;
        Status = status;
    }

    [JsonConstructor]
    public SysResponse(bool status, int genericStatus, string? message, object? data)
    {
        GenericStatus = genericStatus;
        Message = message;
        Data = data;
        Status = status;
    }

    public SysResponse()
    {

    }

    [JsonProperty]
    public int GenericStatus { get; set; }
    [JsonProperty]
    public bool Status { get; set; }
    [JsonProperty]
    public string? Message { get; set; }
    [JsonProperty]
    public object? Data { get; set; }

    [JsonProperty]
    public string[]? Error { get; set; }

    public static SysResponse Succeed(object data, string msg = "Success")
    {
        return new SysResponse(true, 200, msg, data);
    }


    public static SysResponse Failed(string msg, string[]? err = null, int specialStatusCode = 400)
    {
        return new SysResponse
        {
            Error = err,
            Message = msg,
            Status = false,
            GenericStatus = specialStatusCode

        };
    }

}

