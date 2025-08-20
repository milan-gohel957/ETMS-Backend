using BCrypt.Net;

namespace ETMS.Repository.Helpers;
public class HashHelper
{
    // The work factor determines how expensive the hash function is.
    // 12 is a good baseline. Higher values are more secure but slower.
    private static readonly int WorkFactor = 12;

    public static string HashPassword(string password)
    {
        // BCrypt.Net automatically generates a salt and embeds it in the hash.
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public static bool VerifyPassword(string storedHash, string inputPassword)
    {
        try
        {
            // BCrypt.Verify automatically extracts the salt from the storedHash,
            // hashes the inputPassword with it, and performs a constant-time comparison.
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            // This handles cases where the storedHash is not in a valid bcrypt format.
            return false;
        }
    }
}