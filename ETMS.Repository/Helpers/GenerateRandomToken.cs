using System.Security.Cryptography;

namespace ETMS.Repository.Helpers;

public static class GenerateRandomToken
{
    public static string GenerateToken(int size = 32)
    {
        var bytes = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        string base64 = Convert.ToBase64String(bytes);
        return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }
}
