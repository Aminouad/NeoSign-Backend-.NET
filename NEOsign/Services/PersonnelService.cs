using NEOsign.Repositories;

namespace NEOsign.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository _personnelRepository;


        public PersonnelService( IPersonnelRepository personnelRepository)
        {
            _personnelRepository = personnelRepository;
        }

        async public Task<Personnel> AddPersonnel(Personnel personnel, User user)

        {
            return await _personnelRepository.AddPersonnel(personnel,user);
        }
        public ICollection<Personnel> GetAllPersoneelByUser(User user)
        {
            return _personnelRepository.GetAllPersoneelByUser(user);
        }
        public async Task<string> DeletePersonnel(int id)
        {
            return await  _personnelRepository.DeletePersonnel(id);

        }


    }
}
