namespace NEOsign.Repositories
{
    public interface IDocumentRepository
    {
        public Task<Document> ModifyDocument(int id, string name, string description);
        public Task<string> DeleteDocument(int id);
    }
}
