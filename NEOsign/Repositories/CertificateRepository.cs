using Newtonsoft.Json;

namespace NEOsign.Repositories
{
    public class CertificateRepository:ICertificateRepository
    {
        private readonly DataContext _context;

        public CertificateRepository(DataContext context)
        {
            _context = context;
        }
       



        

        public async Task<Certificate> AddCertificate(Certificate certificate)
        {
            if(certificate != null)
            {
                _context.Certificates.Add(certificate);


                await _context.SaveChangesAsync();
                Certificate savedCertificate = new Certificate();
                savedCertificate = await _context.Certificates.FindAsync(certificate.Id);


                return savedCertificate;


            }
            else
            {
                return null;

            }
        }

    }
}
