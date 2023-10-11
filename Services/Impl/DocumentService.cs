using ACBAbankTask.DataModels;
using ACBAbankTask.Repository.Impl;
using ACBAbankTask.Repository.Interfaces;
using ACBAbankTask.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace ACBAbankTask.Services.Impl
{
    public class DocumentService : BaseService, IDocumentService 
    {
        private readonly IDocumentRepository _documentRepository;
        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<int> CreateDocumentAsync(DocumentDto document)
        {
            return await _documentRepository.CreateDocumentAsync(document);
        }
    }
}
