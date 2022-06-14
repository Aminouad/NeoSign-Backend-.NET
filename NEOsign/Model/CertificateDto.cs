namespace NEOsign.Model
{
    public class CertificateDto
    {
        public string Owner { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;

        public IFormFile File { get; set; }
        public IFormFile Image { get; set; }

    }
}
