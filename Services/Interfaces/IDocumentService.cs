﻿using ACBAbankTask.DataModels;

namespace ACBAbankTask.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<int> CreateDocumentAsync(DocumentDto document);
    }
}
