namespace NEOsign.Model
{
    public class DocumentToSign
    {
        public int idDocument { get; set; }
        public string passwordCertificate { get; set; } = string.Empty;

        public string userEmail { get; set; } = string.Empty;
    }
}
