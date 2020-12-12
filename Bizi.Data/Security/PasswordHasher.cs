using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bizi.Data
{
    public class PasswordHasher
    {
        /// <summary>
        /// Hashes and salts the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Tuple of (hash, salt)</returns>
        public static (string, string) HashPassword(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, 20))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] hash = deriveBytes.GetBytes(20);

                string saltString = Convert.ToBase64String(salt);
                string hashString = Convert.ToBase64String(hash);

                return (hashString, saltString);
            }
        }

        public static bool VerifyPassword(string password, string salt, string DBHash)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt)))
            {
                byte[] hash = deriveBytes.GetBytes(20);
                string hashString = Convert.ToBase64String(hash);

                if (hashString == DBHash)
                    return true;
                else
                    return false;
            }
        }
    }
}
