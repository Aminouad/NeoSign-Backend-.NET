using System.ComponentModel.DataAnnotations.Schema;

namespace NEOsign.Model
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Etat { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public User User { get; set; }
        public int? UserId { get; set; }

        public byte[]? Content { get; set; } = null;
        
          
    }
}
