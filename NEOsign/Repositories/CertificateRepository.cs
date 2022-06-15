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


        public  Certificate GetCertificate(int userId)
        {
            return _context.Certificates.SingleOrDefault(c => c.UserId == userId);
        }





        public async Task<Certificate> AddCertificate(Certificate certificate,User owner)
        {
            if(owner.Certificate==null)
            {                 
                _context.Certificates.Add(certificate);
                await _context.SaveChangesAsync();
                Certificate savedCertificate = new Certificate();
                savedCertificate = await _context.Certificates.FindAsync(certificate.Id);
                return savedCertificate;
            }
            else
            {
                var OldCertificate = await _context.Certificates.FindAsync(owner.Certificate.Id);
                OldCertificate.Type = certificate.Type;
                OldCertificate.Path = certificate.Path;
                OldCertificate.PathImage = certificate.PathImage;
                await _context.SaveChangesAsync();
                Certificate savedCertificate = new Certificate();
                savedCertificate = await _context.Certificates.FindAsync(OldCertificate.Id);
                return savedCertificate;
            }
        }

    }
}
