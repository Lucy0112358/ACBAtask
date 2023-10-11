using ACBAbankTask.DataModels;
using ACBAbankTask.Entities;
using ACBAbankTask.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace ACBAbankTask.Repository.Impl
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;
        public AddressRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> SaveAddress(AddressDto address)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                var sqlCommand = new SqlCommand("INSERT INTO Addresses (title, building, street, city, country, customer_id) " +
                    "VALUES (@Title, @Building, @Street, @City, @Country, @CustomerId); " +
                    "SELECT SCOPE_IDENTITY()", connection, transaction);

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@Title", address.Title);
                sqlCommand.Parameters.AddWithValue("@Building", address.Building);
                sqlCommand.Parameters.AddWithValue("@Street", address.Street);
                sqlCommand.Parameters.AddWithValue("@City", address.City);
                sqlCommand.Parameters.AddWithValue("@Country", address.Country);
                sqlCommand.Parameters.AddWithValue("@CustomerId", address.CustomerId);

                sqlCommand.ExecuteNonQuery();

                transaction.Commit();

                return address.CustomerId;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

        }
    }
}
