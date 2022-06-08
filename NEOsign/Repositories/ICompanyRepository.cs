namespace NEOsign.Repositories
{
    public interface ICompanyRepository
    {
        public Task<string> DeleteCompany(int id);

    }
}
