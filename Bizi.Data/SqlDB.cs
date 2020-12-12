using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Bizi.Data
{
    public class SqlDB
    {
        public static async Task<bool> VerifyLogin(string email, string password)
        {
            string storedHash = string.Empty;

            using (SqlConnection conn = new SqlConnection(await AzureKeyVaultService.GetKeyFromVault()))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SELECT userPass from UserAccounts WHERE emailAddress = @email", conn))
                {
                    cmd.Parameters.AddWithValue("email", email);

                    storedHash = (await cmd.ExecuteScalarAsync()).ToString();
                    if (storedHash == null || storedHash == string.Empty)
                        return false;
                }

                //Hash the entered password
                var result = PasswordHasher.HashPassword(password);

                //Compare hashes
                return (storedHash == result.Item1) ? true : false;
            }
        }

        /// <summary>
        /// Retrieves the hash and salt stored in the database for a given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Tuple of (hash, salt)</returns>
        public static async Task<(string, string)> GetDBHashAndSalt(string email)
        {
            string hash = string.Empty;
            string salt = string.Empty;

            using (SqlConnection conn = new SqlConnection(await AzureKeyVaultService.GetKeyFromVault()))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SELECT userPass, userSalt from UserAccounts WHERE emailAddress = @email", conn))
                {
                    cmd.Parameters.AddWithValue("email", email);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            hash = reader.GetString(0);
                            salt = reader.GetString(1);
                        }
                    }
                }
            }

            return (hash, salt);
        }

    }
}
