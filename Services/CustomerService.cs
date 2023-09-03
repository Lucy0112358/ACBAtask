
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using ACBAbankTask.Entities;
using ACBAbankTask.Models;

namespace ACBAbankTask.Services
{
    public class CustomerService
    {
        private readonly string _connectionString;

        public CustomerService(string connectionString)
        {
            _connectionString = "Server=localhost\\SQLEXPRESS;Database=ACBAbank;Trusted_Connection=True;TrustServerCertificate=True";
        }
        public CustomerService()
        {
        }

        public async Task<int> CreateCustomer(CustomerDto customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlCommand = new SqlCommand(
                            "INSERT INTO Customers (Name, Surname, Mobile, Passport, Password, IssuedBy, Address, Email, IssuedAt, CratedAt, UpdatedAt, Birthday) " +
                            "VALUES (@Name, @Surname, @Mobile, @Passport, @Password, @IssuedBy, @Address, @Email, @IssuedAt, @CratedAt, @UpdatedAt, @Birthday); " +
                            "SELECT SCOPE_IDENTITY();",
                            connection,
                            transaction);

                        sqlCommand.Parameters.AddWithValue("@Name", customer.Name);
                        sqlCommand.Parameters.AddWithValue("@Surname", customer.Surname);
                        sqlCommand.Parameters.AddWithValue("@Mobile", customer.Mobile);
                        sqlCommand.Parameters.AddWithValue("@Passport", customer.Passport);
                        sqlCommand.Parameters.AddWithValue("@Password", customer.Password);
                        sqlCommand.Parameters.AddWithValue("@IssuedBy", customer.IssuedBy);
                        sqlCommand.Parameters.AddWithValue("@Address", customer.Address);
                        sqlCommand.Parameters.AddWithValue("@Email", customer.Email);
                        sqlCommand.Parameters.AddWithValue("@IssuedAt", customer.IssuedAt);
                        sqlCommand.Parameters.AddWithValue("@CratedAt", customer.CratedAt);
                        sqlCommand.Parameters.AddWithValue("@UpdatedAt", customer.UpdatedAt);
                        sqlCommand.Parameters.AddWithValue("@Birthday", customer.Birthday);

                        // Execute the INSERT command and return the newly generated customer ID.
                        var customerId = Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());

                        // Commit the transaction.
                        transaction.Commit();

                        return customerId;
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of any error.
                        transaction.Rollback();
                        throw; // Optionally, you can log or handle the exception.
                    }
                }
            }
        }
    }
}
