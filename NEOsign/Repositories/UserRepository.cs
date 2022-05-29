namespace NEOsign.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public  User GetUserByEmail(string email)
        {
             var owner =  _context.Users.SingleOrDefault(a => a.Email == email);
            if(owner != null)
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
