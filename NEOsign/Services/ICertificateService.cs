namespace NEOsign.Services
{
    public interface ICertificateService
    {
        public Task<Certificate> AddCertificate(Certificate certificate,User owner);
        public Certificate GetCertificate(int userId);


    }
}
