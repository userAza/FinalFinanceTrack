using System.Security.Cryptography;
using System.Text;

public static class Security
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
//public static string HashPassword(string password)
//    //{
//    //    using (var sha256 = SHA256.Create())
//    //    {
//    //        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//    //        return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
//    //    }
//    //}

