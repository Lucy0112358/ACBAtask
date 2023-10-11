using System.Transactions;
using ACBAbankTask.DataModels;
using ACBAbankTask.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace ACBAbankTask.Repository.Impl
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;
        public DocumentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateDocumentAsync(DocumentDto document)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();
            try
            {
                var sqlCommand = new SqlCommand("INSERT INTO Documents (title, issued_at, expires_at, issued_by, number, created_at, updated_at, customer_id) "+
                   "VALUES (@Title, @IssuedAt, @ExpiresAt, @IssuedBy, @Number, @CreatedAt, @UpdatedAt, @CustomerId); " +
                "SELECT SCOPE_IDENTITY()", connection,transaction);

                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.AddWithValue("@Title", document.Title);
                sqlCommand.Parameters.AddWithValue("@IssuedAt", document.IssuedAt);
                sqlCommand.Parameters.AddWithValue("@ExpiresAt", document.ExpiresAt);
                sqlCommand.Parameters.AddWithValue("@IssuedBy", document.IssuedBy);
                sqlCommand.Parameters.AddWithValue("@Number", document.Number);
                sqlCommand.Parameters.AddWithValue("@CreatedAt", document.CreatedAt);
                sqlCommand.Parameters.AddWithValue("@UpdatedAt", document.UpdatedAt);
                sqlCommand.Parameters.AddWithValue("@CustomerId", document.customerId);
                var documentId = Convert.ToInt32(await sqlCommand.ExecuteScalarAsync());

                transaction.Commit();

                return documentId;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

        }
    }
}
