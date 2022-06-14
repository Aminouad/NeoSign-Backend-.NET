namespace NEOsign.Services
{
    public interface IDocumentService
    {
        public Task<Document> AddDocument(Document document);
        public ICollection<Document> GetDocumentsByUser(int userId);
        public Task<Document> ModifyDocument(int id, string name, string description);
        public Task<string> DeleteDocument(int id);
        public Document GetDocumentById(int idDocument);


    }
}
