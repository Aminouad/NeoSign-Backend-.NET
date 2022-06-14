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
        public async Task<Company> AddCompany(Company company, User user)
        {
            return await _companyRepositorie.AddCompany(company, user);
        }
        public async Task<ICollection<Company>> GetAllCompany()
        {
            return await _companyRepositorie.GetAllCompany();       
        }

    }
}
