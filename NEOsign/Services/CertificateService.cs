using NEOsign.Repositories;

namespace NEOsign.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;


        public CertificateService(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }
        public Task<Certificate> AddCertificate(Certificate certificate)
        {
           return _certificateRepository.AddCertificate(certificate);
        }
        public Certificate GetCertificate(int userId)
        {
            return _certificateRepository.GetCertificate(userId);
        }

    }
}
