using ACBAbankTask.DataModels;
using ACBAbankTask.Entities;
using ACBAbankTask.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace ACBAbankTask.Repository.Impl
{
    public class MobileRepository : IMobileRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;
        public MobileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> SaveMobile(MobileDto number)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                var sqlCommand = new SqlCommand("INSERT INTO Mobile (title, country_code, number, customer_id) " +
                    "VALUES (@Title, @CountryCode, @Number, @CustomerId); " +
                    "SELECT SCOPE_IDENTITY()", connection, transaction);

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@Title", number.Title);
                sqlCommand.Parameters.AddWithValue("@CountryCode", number.CountryCode);
                sqlCommand.Parameters.AddWithValue("@Number", number.Number);
                sqlCommand.Parameters.AddWithValue("@CustomerId", number.CustomerId);

                sqlCommand.ExecuteNonQuery();
            transaction.Commit();

            return number.CustomerId;
        }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

}
    }
}
