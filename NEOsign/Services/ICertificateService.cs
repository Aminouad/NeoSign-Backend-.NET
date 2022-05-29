namespace NEOsign.Services
{
    public interface ICertificateService
    {
        public Task<Certificate> AddCertificate(Certificate certificate);
        
    }
}
