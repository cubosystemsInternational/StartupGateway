/**
 * Created by: Ibrahim
 * Created on: 21/03/2024
 * Description: Business logic class for DocumentBLL .
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Model;
using StartupGateway.UoW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to the Document entity.
    /// </summary>
    public class DocumentBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DocumentBLL> _logger;

        /// <summary>
        /// Constructor to initialize DocumentBLL with necessary dependencies.
        /// </summary>
        public DocumentBLL(IUnitOfWork unitOfWork, ILogger<DocumentBLL> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the document details for the specified document ID.
        /// </summary>
        /// <param name="documentId">The ID of the document to retrieve.</param>
        /// <returns>Document details.</returns>
        public Document GetDocumentById(int documentId)
        {
            try
            {
                using var documentRepository = _unitOfWork.GetDAL<IDocumentDAL>();
                return documentRepository.GetEntityById(documentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving document by ID: {DocumentId}", documentId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves all documents.
        /// </summary>
        /// <returns>List of document details.</returns>
        public List<Document> GetAllDocuments()
        {
            try
            {
                _logger.LogInformation("In GetAllDocuments");
                using var documentRepository = _unitOfWork.GetDAL<IDocumentDAL>();
                return documentRepository.GetAllRecords().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all documents.");
                throw;
            }
        }

        /// <summary>
        /// Adds a new document.
        /// </summary>
        /// <param name="document">The document to add.</param>
        /// <returns>True if the document was added successfully, otherwise false.</returns>
        public bool AddDocument(Document document)
        {
            try
            {
                using var documentRepository = _unitOfWork.GetDAL<IDocumentDAL>();
                document.Status = EntityStatus.Active;
                document.ModifiedOn = DateTime.Now;
                documentRepository.AddEntity(document);
                _unitOfWork.Commit();
                _logger.LogInformation("Document added successfully: {DocumentId}.", document.DocumentId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding document: {DocumentId}.", document.DocumentId);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing document.
        /// </summary>
        /// <param name="updatedDocument">The updated document information.</param>
        /// <returns>The updated document details.</returns>
        public object UpdateDocument(Document updatedDocument)
        {
            try
            {
                using var documentRepository = _unitOfWork.GetDAL<IDocumentDAL>();
                var existingDocument = documentRepository.GetEntityById(updatedDocument.DocumentId);

                if (existingDocument != null)
                {
                    existingDocument.DocumentTitle = updatedDocument.DocumentTitle;
                    existingDocument.DocumentBody = updatedDocument.DocumentBody ?? existingDocument.DocumentBody;
                    existingDocument.DocumentContent = updatedDocument.DocumentContent;
                    existingDocument.DocumentType = updatedDocument.DocumentType;
                    existingDocument.Status = updatedDocument.Status;
                    existingDocument.ModifiedBy = updatedDocument.ModifiedBy;
                    existingDocument.ModifiedOn = DateTime.Now; // Update modified date

                    documentRepository.UpdateEntity(existingDocument);
                    _unitOfWork.Commit();
                    _logger.LogInformation("Document Updated successfully: {DocumentId}.", existingDocument.DocumentId);
                    return existingDocument;
                }

                throw new Exception("Document not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating document with ID: {DocumentId}.", updatedDocument.DocumentId);
                throw;
            }
        }
    }
}
