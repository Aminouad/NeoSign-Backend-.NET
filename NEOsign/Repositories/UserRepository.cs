namespace NEOsign.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

      async  public Task<string> DeleteUser(int id)
        {
             User user = _context.Users.Find(id);
              if(user == null)
            {
                return "user not found";
            }
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return "User deleted";
        }

        public  User GetUserByEmail(string email)
        {
            var owner = _context.Users.Where(u => u.Email == email).Include(u => u.Certificate).SingleOrDefault();
            if (owner != null)
            {
                return owner;
            }
            else
            {
                return null;
            }

        }
    }
}
