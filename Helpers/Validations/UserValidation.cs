using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ACBAbankTask.Helpers.Validations
{
    public class UserValidation
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;

        public UserValidation(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public UserValidation() { }

        public bool IsEmailUnique(string email)
    {

            using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();

                int count = Convert.ToInt32(command.ExecuteScalar());

                return count == 0;
            }
        }
    }

}
}
