namespace NEOsign.Repositories
{
    public interface ICompanyRepository
    {
        public Task<string> DeleteCompany(int id);
        public  Task<Company> AddCompany(Company company,User user);
        public Task<ICollection<Company>> GetAllCompany();

    }
}
