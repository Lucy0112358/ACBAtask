using System.Collections.Generic;
using System.Data;
using ACBAbankTask.DataModels;
using ACBAbankTask.Models;
using ACBAbankTask.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace ACBAbankTask.Repository.Impl
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

        }
        public async Task<int> CreateCustomer(CustomerDto customer, List<DocumentDto> documents, List<AddressDto> address, List<MobileDto> mobile)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            try
            {
                var sqlCommand = new SqlCommand(
                    "INSERT INTO Customers (name, surname, email, created_at, updated_at, birthday, gender) " +
                    "VALUES (@Name, @Surname, @Email, @CreatedAt, @UpdatedAt, @Birthday, @Gender); " +
                    "SELECT SCOPE_IDENTITY();",
                    connection,
                    transaction);

                sqlCommand.Parameters.AddWithValue("@Name", customer.Name);
                sqlCommand.Parameters.AddWithValue("@Surname", customer.Surname);
                sqlCommand.Parameters.AddWithValue("@Email", customer.Email);
                sqlCommand.Parameters.AddWithValue("@CreatedAt", customer.CreatedAt);
                sqlCommand.Parameters.AddWithValue("@UpdatedAt", customer.UpdatedAt);
                sqlCommand.Parameters.AddWithValue("@Birthday", customer.Birthday);
                sqlCommand.Parameters.AddWithValue("@Gender", customer.Gender);
                var customerId = Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());

                foreach (var document in documents)
                {
                    sqlCommand.CommandText = "INSERT INTO Documents (title, issued_at, expires_at, issued_by, number, created_at, updated_at, customer_id) " +
                    "VALUES (@Title, @IssuedAt, @ExpiresAt, @IssuedBy, @Number, @CreatedAt, @UpdatedAt, @CustomerId); " +
                    "SELECT SCOPE_IDENTITY()";

                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@Title", document.Title);
                    sqlCommand.Parameters.AddWithValue("@IssuedAt", document.IssuedAt);
                    sqlCommand.Parameters.AddWithValue("@ExpiresAt", document.ExpiresAt);
                    sqlCommand.Parameters.AddWithValue("@IssuedBy", document.IssuedBy);
                    sqlCommand.Parameters.AddWithValue("@Number", document.Number);
                    sqlCommand.Parameters.AddWithValue("@CreatedAt", document.CreatedAt);
                    sqlCommand.Parameters.AddWithValue("@UpdatedAt", document.UpdatedAt);
                    sqlCommand.Parameters.AddWithValue("@CustomerId", customerId);

                    sqlCommand.ExecuteNonQuery();
                }

                foreach (var addresses in address)
                {
                    sqlCommand.CommandText = "INSERT INTO Addresses (title, building, street, city, country, customer_id) " +
                    "VALUES (@Title, @Building, @Street, @City, @Country, @CustomerId); " +
                    "SELECT SCOPE_IDENTITY()";

                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@Title", addresses.Title);
                    sqlCommand.Parameters.AddWithValue("@Building", addresses.Building);
                    sqlCommand.Parameters.AddWithValue("@Street", addresses.Street);
                    sqlCommand.Parameters.AddWithValue("@City", addresses.City);
                    sqlCommand.Parameters.AddWithValue("@Country", addresses.Country);
                    sqlCommand.Parameters.AddWithValue("@CustomerId", customerId);

                    sqlCommand.ExecuteNonQuery();
                }

                foreach (var numbers in mobile)
                {
                    sqlCommand.CommandText = "INSERT INTO Mobile (title, country_code, number, customer_id) " +
                    "VALUES (@Title, @CountryCode, @Number, @CustomerId); " +
                    "SELECT SCOPE_IDENTITY()";

                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@Title", numbers.Title);
                    sqlCommand.Parameters.AddWithValue("@CountryCode", numbers.CountryCode);
                    sqlCommand.Parameters.AddWithValue("@Number", numbers.Number);
                    sqlCommand.Parameters.AddWithValue("@CustomerId", customerId);

                    sqlCommand.ExecuteNonQuery();
                }

                transaction.Commit();

                return customerId;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task<bool> EditCustomerAsync(int customerId, CustomerDto updatedCustomer)
        {
            using (var connection = new SqlConnection(_connectionString))

            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = "UPDATE Customers SET name = @Name, surname = @Surname, email = @Email, updated_at = @UpdatedAt,Birthday = @Birthday WHERE Id = @customerId";
                        var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId };

                        var parameters = new SqlParameter[]
                        {
                    customerIdParameter,
                            new SqlParameter("@Name", SqlDbType.NVarChar) { Value = updatedCustomer.Name },
                            new SqlParameter("@Surname", SqlDbType.NVarChar) { Value = updatedCustomer.Surname },
                            new SqlParameter("@Email", SqlDbType.NVarChar) { Value = updatedCustomer.Email },
                            new SqlParameter("@UpdatedAt", SqlDbType.DateTime) { Value = updatedCustomer.UpdatedAt },
                            new SqlParameter("@Birthday", SqlDbType.DateTime) { Value = updatedCustomer.Birthday },
                    };

                        using (var command = new SqlCommand(sql, connection, transaction))
                        {
                            command.Parameters.AddRange(parameters);
                            await command.ExecuteNonQueryAsync();
                            transaction.Commit();
                            return true;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))

            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = new SqlCommand("DELETE FROM Customers WHERE Id = @CustomerId", connection, transaction))
                        {
                            command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });
                            await command.ExecuteNonQueryAsync();
                        }
                        using (var command = new SqlCommand("DELETE FROM Documents WHERE customer_id = @CustomerId", connection, transaction))
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });
                            await command.ExecuteNonQueryAsync();
                        }
                        using (var command = new SqlCommand("DELETE FROM Addresses WHERE customer_id = @CustomerId", connection, transaction))
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });
                            await command.ExecuteNonQueryAsync();
                        }
                        using (var command = new SqlCommand("DELETE FROM Mobile WHERE customer_id = @CustomerId", connection, transaction))
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });
                            await command.ExecuteNonQueryAsync();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public List<object> SearchCustomers(string name, string surname, string email, string mobile, int pageNumber, string document)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // string query = $"SELECT * FROM Customers JOIN Mobile ON Customers.id = Mobile.customer_id JOIN Documents ON Customers.Id = Documents.customer_id WHERE Mobile.number LIKE '{mobile}%' OR Customers.name LIKE '{name}%' OR Customers.email LIKE '{email}%' OR Customers.surname LIKE '{surname}%' OR Documents.number LIKE '{document}%' ORDER BY Customers.Id OFFSET 2*'{pageNumber - 1}' ROWS FETCH NEXT 2 ROWS ONLY";

                using (SqlCommand command = new SqlCommand("SearchCustomers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@mobile", mobile);
                    command.Parameters.AddWithValue("@pageNumber", pageNumber);
                    command.Parameters.AddWithValue("@document", document);

                    using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<object> results = new List<object>();
                    HashSet<int> uniqueCustomers = new HashSet<int>();
                    while (reader.Read())
                    {
                        int customerId = (int)reader["id"];
                        if (!uniqueCustomers.Contains(customerId))
                        {
                            var customer = new
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Surname = reader.GetString(reader.GetOrdinal("Surname")),
                                Mobile = reader["Number"] != DBNull.Value ? reader["Number"] : "Number does not exist."
                            };
                            results.Add(customer);
                            uniqueCustomers.Add(customerId);
                        }

                    }
                    return results;
                }
                }
            }
        }
    }
}
