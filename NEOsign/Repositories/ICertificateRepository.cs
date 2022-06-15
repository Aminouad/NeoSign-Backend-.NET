namespace NEOsign.Repositories
{
    public interface ICertificateRepository
    {
        public Task<Certificate> AddCertificate(Certificate certificate, User owner);
        public Certificate GetCertificate(int userId);

    }
}
