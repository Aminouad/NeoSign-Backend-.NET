namespace NEOsign.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext _context;

        public DocumentRepository(DataContext context)
        {
            _context = context;
        }

       async public Task<string> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if(document == null)
                return null;

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return "deleted";



        }

        async public Task<Document> ModifyDocument(int id,string name, string description)
        {

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return null;
            }
            else {
                if (name == null & description != null)
                {
                    document.Description = description;
                    await _context.SaveChangesAsync();


                }
                else if (name == null & description == null)
                {
                    return document;

                }
                else if (name != null & description == null)
                {
                    document.Name = name;
                    await _context.SaveChangesAsync();

                    return document;
                }

                else
                {
                    document.Name = name;
                    document.Description = description;
                    await _context.SaveChangesAsync();

                    return document;

                }
                }


            return document;









        }
    }
}
