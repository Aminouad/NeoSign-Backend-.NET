namespace NEOsign.Services
{
    public interface IUserService
    {
        object GetUserInfo();
        public User GetUserByEmail(string email);
        

    }
}
