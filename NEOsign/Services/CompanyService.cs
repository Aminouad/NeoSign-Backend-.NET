using NEOsign.Repositories;

namespace NEOsign.Services
{
    public class CompanyService:ICompanyService
    {
        private readonly ICompanyRepository _companyRepositorie;


        public CompanyService(ICompanyRepository companyRepositorie)
        {
            _companyRepositorie = companyRepositorie;
        }
        
        async public Task<string> DeleteCompany(int id)
        {
            return await _companyRepositorie.DeleteCompany(id);
        }
    }
}
