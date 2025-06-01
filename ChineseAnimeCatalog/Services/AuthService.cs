using ChineseAnimeCatalog.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ChineseAnimeCatalog.Services
{
    public class AuthService
    {
        private readonly DatabaseContext _db;

        public AuthService(DatabaseContext db)
        {
            _db = db;
        }

        public User? GetUserByUsername(string username)
        {
            using var connection = _db.CreateConnection();
            connection.Open();

            var command = new SqlCommand("SELECT * FROM Users WHERE Username = @username", (SqlConnection)connection);
            command.Parameters.AddWithValue("@username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                    Role = reader.GetString(reader.GetOrdinal("Role"))
                };
            }
            return null;
        }

        public void RegisterUser(string username, string password, string role = "User")
        {
            string hash = BCryptNet.HashPassword(password);

            using var connection = _db.CreateConnection();
            connection.Open();

            var command = new SqlCommand(
                "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@username, @hash, @role)",
                (SqlConnection)connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@hash", hash);
            command.Parameters.AddWithValue("@role", role);

            command.ExecuteNonQuery();
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return BCryptNet.Verify(enteredPassword, storedHash);
        }
    }
}