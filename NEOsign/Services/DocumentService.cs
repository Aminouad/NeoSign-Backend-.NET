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
        async public Task<string> DeleteDocument(int id)
        {
            return await _documentRepositorie.DeleteDocument(id);
        }

    }
}
