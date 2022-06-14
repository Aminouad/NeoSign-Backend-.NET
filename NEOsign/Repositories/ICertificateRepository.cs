namespace NEOsign.Repositories
{
    public interface ICertificateRepository
    {
        public Task<Certificate> AddCertificate(Certificate certificate);
        public Certificate GetCertificate(int userId);

    }
}
