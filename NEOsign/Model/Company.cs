namespace NEOsign.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Etat { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public User User { get; set; }
        public int UserId { get; set; }


    }
}
