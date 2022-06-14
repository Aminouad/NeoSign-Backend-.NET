using Newtonsoft.Json;
using System.Collections.Generic;

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
            if (document == null)
                return null;

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return "deleted";



        }
        async public Task<Document> AddDocument(Document document)
        {
            _context.Documents.Add(document);
            try
            {
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                var addedDocument =await _context.Documents.FindAsync(document.Id);
                return addedDocument;

            }
            catch (Exception e)
            {
                throw new Exception("AddDocument",e);
            }
        }

        public Document GetDocumentById(int idDocument)
            {
                if (idDocument != null)
                {

                    return _context.Documents.Find(idDocument);
                }
                else
                {
                    return null;
                }

            }


             public async Task<Document> ModifyDocument(int id, string name, string description)
            {

                var document = await _context.Documents.FindAsync(id);
                if (document == null)
                {
                    return null;
                }
                else
                {
                    if (name == String.Empty & description != String.Empty)
                    {
                        document.Description = description;
                        await _context.SaveChangesAsync();
                    }
                    else if (name == String.Empty & description == String.Empty)
                    {
                        return document;
                    }
                    else if (name != String.Empty & description == String.Empty)
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
        public  ICollection<Document> GetDocumentsByUser(int userId)
        {
            ICollection<Document> documents;
            documents =  _context.Documents.
                Where(d => d.User.Id == userId).
                Include(d => d.User).Select(d => new Document
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = d.Type,
                    Description = d.Description,
                    Etat = d.Etat,
                    Date = d.Date,
                    Path = d.Path,
                    User = d.User
                }).ToList();

            return documents;

        }
    }
    }
