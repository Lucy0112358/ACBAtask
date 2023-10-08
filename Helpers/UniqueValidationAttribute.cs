using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace ACBAbankTask.Helpers
{
    public class UniqueValidationAttribute : ValidationAttribute
    {
        private readonly string propertyNameToCheck;
        private readonly string column;
        // Create a configuration object
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public UniqueValidationAttribute(string propertyNameToCheck, string column)
        {
            this.propertyNameToCheck = propertyNameToCheck;
            this.column = column;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Retrieve the connection string
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string tableName = "Customers";
            var propertyValue = value as string;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (propertyNameToCheck == "Email")
            {

                string sqlQuery = $"SELECT TOP 1 1 FROM {tableName} WHERE {column} = @ValueToCheck";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ValueToCheck", propertyValue);

                    bool valueExists = command.ExecuteScalar() != null;

                    if (valueExists)
                    {
                        return new ValidationResult($"{propertyNameToCheck} already exists in the database.");
                    }
                }
            }

            if (propertyNameToCheck == "Passport")
            {

                string sqlQuery = $"SELECT TOP 1 1 FROM {tableName} WHERE {column} = @ValueToCheck";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ValueToCheck", propertyValue);

                    bool valueExists = command.ExecuteScalar() != null;

                    if (valueExists)
                    {
                        return new ValidationResult($"{propertyNameToCheck} already exists in the database.");
                    }
                }
            }
            connection.Close();

            return ValidationResult.Success;
        }
    }
}
