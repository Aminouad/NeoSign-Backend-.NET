using System.ComponentModel.DataAnnotations.Schema;

namespace NEOsign.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Etat { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Company> Companies { get; set; }
        public Certificate? Certificate { get; set; }



    }
}
