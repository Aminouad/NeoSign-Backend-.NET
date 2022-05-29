namespace NEOsign.Services
{
    public interface IDocumentService
    {
         public Task<Document> ModifyDocument(int id, string name, string description);
        public Task<string> DeleteDocument(int id);

    }
}
