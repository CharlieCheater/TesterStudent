using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TesterStudent.Domain;

public static class PasswordBuilder
{
    public static async Task<string> CreatePasswordAsync(string password, DateTime date)
    {
        BuildInfo info = new BuildInfo()
        {
            Timestamp = new DateTimeOffset(date).ToUnixTimeMilliseconds(),
        };
        var data = "login." + JsonSerializer.Serialize(info);
        return await GetHmacSHA256(password, data);
    }
    private static async Task<string> GetHmacSHA256(string key, string data)
    {
        string hash;
        ASCIIEncoding encoder = new ASCIIEncoding();
        byte[] code = encoder.GetBytes(key);
        using (HMACSHA256 hmac = new HMACSHA256(code))
        {
            using (var ms = new MemoryStream(encoder.GetBytes(data)))
            {
                byte[] hmBytes = await hmac.ComputeHashAsync(ms);
                hash = ToHexString(hmBytes);
            }
        }
        return hash;
    }
    private static string ToHexString(byte[] array)
    {
        StringBuilder hex = new StringBuilder(array.Length * 2);
        foreach (byte b in array)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }
}