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
            User user = _context.Users.Where(u => u.Email == company.Contact).Include(u => u.Personnels).SingleOrDefault(); 

            var personnels = _context.Users.Where(p => p.Master == user.Role);
           _context.Users.RemoveRange(personnels);
           // await _context.SaveChangesAsync();
            // User user = _context.Users.OrderBy(e => e.Email == company.Contact).Include(e => e.Documents).First();          
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return "deleted";
        }
        public async Task<Company> AddCompany(Company company, User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }
        public async Task<ICollection<Company>> GetAllCompany()
        {
           return  await _context.Companies.ToListAsync();
        }


    }
}
