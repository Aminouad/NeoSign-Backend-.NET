namespace NEOsign.Repositories
{
    public class PersonnelRepository : IPersonnelRepository
    {
        private readonly DataContext _context;

        public PersonnelRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Personnel> AddPersonnel(Personnel personnel,User user)
        {         
            _context.Personnels.Add(personnel);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return await _context.Personnels.SingleOrDefaultAsync(u => u.Email == personnel.Email);
        }
        public ICollection<Personnel> GetAllPersoneelByUser(User user)
        {
            return  _context.Personnels.Where(d => d .User == user).ToList();
        }
        public async Task<string> DeletePersonnel(int id)
        {
            var personnel = await _context.Personnels.FindAsync(id);
            if (personnel == null)
                return "error";
            User user = _context.Users.Single(a => a.Email == personnel.Email);
            // User user = _context.Users.OrderBy(e => e.Email == company.Contact).Include(e => e.Documents).First();          
            _context.Personnels.Remove(personnel);
            await _context.SaveChangesAsync();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return "deleted";
        }

    }
}
