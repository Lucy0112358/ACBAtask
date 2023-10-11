using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class UniqueValidationAttribute : ValidationAttribute
{
    private readonly string propertyNameToCheck;
    private readonly string column;
    private readonly string idToExclude; 

    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    public UniqueValidationAttribute(string propertyNameToCheck, string column, string idToExclude = null) // Updated constructor
    {
        this.propertyNameToCheck = propertyNameToCheck;
        this.column = column;
        this.idToExclude = idToExclude;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        string id = GetIdFromValidationContext(validationContext);

        if (id == idToExclude)
        {
            return ValidationResult.Success;
        }

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        string tableName = "Customers";
        var propertyValue = value as string;

        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string sqlQuery = $"SELECT TOP 1 1 FROM {tableName} WHERE {column} = @ValueToCheck";

        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        {
            command.Parameters.AddWithValue("@ValueToCheck", propertyValue);

            bool valueExists = command.ExecuteScalar() != null;

            if (valueExists)
            {
                return new ValidationResult($"{propertyNameToCheck} արդեն կա տվյալների բազայում");
            }
        }

        connection.Close();

        return ValidationResult.Success;
    }

    private string GetIdFromValidationContext(ValidationContext validationContext)
    {
        var idProperty = validationContext.ObjectType.GetProperty("Id");

        if (idProperty != null)
        {
            return idProperty.GetValue(validationContext.ObjectInstance)?.ToString();
        }

        return null;
    }
}
