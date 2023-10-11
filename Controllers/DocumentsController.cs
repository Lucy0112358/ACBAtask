using ACBAbankTask.DataModels;
using ACBAbankTask.Helpers.Validations;
using ACBAbankTask.Services.Impl;
using ACBAbankTask.Services.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACBAbankTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly DocumentValidation _validDoc;

        public DocumentsController(DocumentValidation validDoc, IDocumentService documentService)
        {
            _documentService = documentService;
            _validDoc = validDoc;
        }


        // POST api/<DocumentsController>
        [HttpPost("save_document")]
        public async Task<IActionResult> PostAsync([FromBody] DocumentDto document)
        {
            var validationResult = _validDoc.ValidateDocument(document);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            int documentId = await _documentService.CreateDocumentAsync(document);
            return Ok(document);
        }
    }
}
