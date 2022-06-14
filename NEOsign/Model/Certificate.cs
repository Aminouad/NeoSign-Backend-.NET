namespace NEOsign.Model
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }

        public string PathImage { get; set; }

        public byte[]? Content { get; set; } = null;

        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
