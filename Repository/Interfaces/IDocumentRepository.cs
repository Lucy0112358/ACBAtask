using ACBAbankTask.DataModels;

namespace ACBAbankTask.Repository.Interfaces
{
    public interface IDocumentRepository
    {
        Task<int> CreateDocumentAsync(DocumentDto document);
    }
}
