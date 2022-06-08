namespace NEOsign.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<string> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return "error";
            User user =  _context.Users.Single(a => a.Email == company.Contact);
            // User user = _context.Users.OrderBy(e => e.Email == company.Contact).Include(e => e.Documents).First();
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return "deleted";
        }
    }
}
