namespace NEOsign.Model
{
    public class Personnel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public User User { get; set; }
        public int UserId { get; set; }
       
    }
}
