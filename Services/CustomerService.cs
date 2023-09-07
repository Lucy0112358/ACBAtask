
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using ACBAbankTask.Entities;
using ACBAbankTask.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
            using (var connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=ACBAbank;Trusted_Connection=True;TrustServerCertificate=True"))
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

        public async Task<bool> EditCustomerAsync(int customerId, CustomerDto updatedCustomer)
        {
            using (var connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=ACBAbank;Trusted_Connection=True;TrustServerCertificate=True"))

            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Your update SQL command here.
                        var sql = "UPDATE Customers SET Name = @Name, Surname = @Surname, Mobile = @Mobile,Passport = @Passport,Password = @Password,IssuedBy = @IssuedBy,Address = @Address,Email = @Email,IssuedAt = @IssuedAt,UpdatedAt = @UpdatedAt,Birthday = @Birthday WHERE Id = @customerId";
                        var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId };

                        var parameters = new SqlParameter[]
                        {
                    customerIdParameter,
                            new SqlParameter("@Name", SqlDbType.NVarChar) { Value = updatedCustomer.Name },
                            new SqlParameter("@Surname", SqlDbType.NVarChar) { Value = updatedCustomer.Surname },
                            new SqlParameter("@Mobile", SqlDbType.NVarChar) { Value = updatedCustomer.Mobile },
                            new SqlParameter("@Passport", SqlDbType.NVarChar) { Value = updatedCustomer.Passport },
                            new SqlParameter("@Password", SqlDbType.NVarChar) { Value = updatedCustomer.Password },
                            new SqlParameter("@IssuedBy", SqlDbType.NVarChar) { Value = updatedCustomer.IssuedBy },
                            new SqlParameter("@Address", SqlDbType.NVarChar) { Value = updatedCustomer.Address },
                            new SqlParameter("@Email", SqlDbType.NVarChar) { Value = updatedCustomer.Email },
                            new SqlParameter("@IssuedAt", SqlDbType.DateTime) { Value = updatedCustomer.IssuedAt },
                            new SqlParameter("@UpdatedAt", SqlDbType.DateTime) { Value = updatedCustomer.UpdatedAt },
                            new SqlParameter("@Birthday", SqlDbType.DateTime) { Value = updatedCustomer.Birthday },
                    };

                        // Execute the SQL command within the transaction.
                        using (var command = new SqlCommand(sql, connection, transaction))
                        {
                            command.Parameters.AddRange(parameters);
                            var affectedRows = await command.ExecuteNonQueryAsync();

                            if (affectedRows > 0)
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw; // Re-throw the exception to handle it at a higher level.
                    }
                }
            }
        }
        public async Task<List<object>> SearchCustomers(string name = null, string surname = null, string email = null, string mobile = null)
        {
            using (var connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=ACBAbank;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                connection.Open();

                string query = $"SELECT * FROM Customers WHERE Mobile LIKE '{mobile}%' OR Surname LIKE '{surname}%' OR Name LIKE '{name}%' OR Email LIKE '{email}%'";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<object> results = new List<object>();
                    while (reader.Read())
                    {
                        var customer = new
                        {
                         //   Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Surname = reader.GetString(reader.GetOrdinal("Surname")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Mobile = reader.GetString(reader.GetOrdinal("Mobile")),
                            Passport = reader.GetString(reader.GetOrdinal("Passport")),
                        };

                        results.Add(customer);
                    }

                    return results;
                }
            }
        }



    }
}
