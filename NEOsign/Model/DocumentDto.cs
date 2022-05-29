using System.ComponentModel.DataAnnotations;

namespace NEOsign.Model
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Etat { get; set; } = string.Empty;
       
        public IFormFile File { get; set; }
    }
}
