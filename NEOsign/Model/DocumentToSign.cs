namespace NEOsign.Model
{
    public class DocumentToSign
    {
        public int idDocument { get; set; }
        public string passwordCertificate { get; set; } = string.Empty;
        public string typeOfSignature { get; set; } = string.Empty;
        public string nature { get; set; } = string.Empty;
        public string label { get; set; } = string.Empty;

        public string userEmail { get; set; } = string.Empty;
    }
}
