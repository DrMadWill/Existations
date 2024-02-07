using System.Security.Cryptography;
using System.Text;

namespace DrMadWill.Extensions.Explorer;

public static class ExplorerHelper 
{
    public static string GenerateHash( string key,string code)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        using var sha = SHA256.Create();
        return Convert.ToBase64String( hmac.ComputeHash( sha.ComputeHash(Encoding.UTF8.GetBytes(code))));
    }

    public static bool ValidationHash(string key, string code, string providedHash)
    {
        return providedHash == GenerateHash(key, code);
    }

    public static string? GetOperation(this Type type)
    {
        var attr = type.GetCustomAttributes(true)
            .FirstOrDefault(a => a.GetType() == typeof(SysDefinitionAttribute)) 
                as SysDefinitionAttribute;

        return attr?.Operation.ToString();
    } 
    

}