using System;
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

        /// <summary>
        /// Determines if a user has already registered an email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Boolean</returns>
        public static async Task<bool> IsEmailTaken(string email)
        {
            using (SqlConnection conn = new SqlConnection(await AzureKeyVaultService.GetKeyFromVault()))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(emailAddress) from UserAccounts WHERE emailAddress = @email", conn))
                {
                    cmd.Parameters.AddWithValue("email", email);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                       await reader.ReadAsync();
                       return (reader.GetInt32(0) > 0) ? true : false;
                    }
                }
            }
        }

        public static async Task InsertUser(Guid id, string pass, string salt, string email, string first, string last)
        {
            using (SqlConnection conn = new SqlConnection(await AzureKeyVaultService.GetKeyFromVault()))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO UserAccounts(userID, userPass, userSalt, emailAddress, firstName, lastName) VALUES(@id, @pass, @salt, @email, @first, @last);", conn))
                {
                    cmd.Parameters.AddWithValue("id", id.ToString());
                    cmd.Parameters.AddWithValue("pass", pass);
                    cmd.Parameters.AddWithValue("salt", salt);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("first", first);
                    cmd.Parameters.AddWithValue("last", last);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<bool> IsGUIDConflict(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(await AzureKeyVaultService.GetKeyFromVault()))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SELECT userID FROM UserAccounts WHERE userID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);

                    var match = await cmd.ExecuteScalarAsync();

                    return (match != null) ? true : false;
                }
            }
        }

    }
}
