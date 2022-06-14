namespace NEOsign.Repositories
{
    public interface IPersonnelRepository
    {
        public  Task<Personnel> AddPersonnel(Personnel personnel, User user);
        public ICollection<Personnel> GetAllPersoneelByUser(User user);
        public  Task<string> DeletePersonnel(int id);


    }
}
