namespace NEOsign.Services
{
    public interface ICompanyService
    {
        public Task<string> DeleteCompany(int id);
    }
}
