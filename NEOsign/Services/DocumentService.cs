using NEOsign.Repositories;

namespace NEOsign.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepositorie;

        
            public DocumentService( IDocumentRepository documentRepositorie)
        {
            _documentRepositorie = documentRepositorie;
        }
        async public Task<Document> ModifyDocument(int id, string name, string description)
        {
            return await _documentRepositorie.ModifyDocument(id,name,description);
        }
        public ICollection<Document> GetDocumentsByUser(int userId)
        {
            return _documentRepositorie.GetDocumentsByUser(userId);
        }

        async public Task<string> DeleteDocument(int id)
        {
            return await _documentRepositorie.DeleteDocument(id);
        }
        public Document GetDocumentById(int idDocument)
        {
            return _documentRepositorie.GetDocumentById(idDocument);
        }
        public Task<Document> AddDocument(Document document)
        {
            return  _documentRepositorie.AddDocument(document);
        }



    }
}
