namespace NEOsign.Repositories
{
    public interface IUserRepository
    {
         public User GetUserByEmail(string email);
       

    }
}
