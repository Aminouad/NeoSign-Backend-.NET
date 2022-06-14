namespace NEOsign.Services
{
    public interface IPersonnelService
    {
        public Task<Personnel> AddPersonnel(Personnel personnel, User user);
        public ICollection<Personnel> GetAllPersoneelByUser(User user);
        public Task<string> DeletePersonnel(int id);

    }
}
