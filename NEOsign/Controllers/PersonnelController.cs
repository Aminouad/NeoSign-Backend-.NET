using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEOsign.Model;
using Newtonsoft.Json;

namespace NEOsign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly DataContext _context;
        private readonly ICompanyService _companyService;
        private readonly IPersonnelService _personnelService;



        public PersonnelController(IConfiguration configuration, IUserService userService, DataContext context, ICompanyService companyService, IPersonnelService personnelService)
        {
            _configuration = configuration;
            _userService = userService;
            _context = context;
            _companyService = companyService;
            _personnelService = personnelService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(PersonnelDto request)
        {
            var user = new User();          
            AuthenticationController.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Email = request.Email;
            user.Role = request.Position;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;           
            var personnel = new Personnel();
            personnel.Name = request.Name;
            personnel.Email = request.Email;   
            personnel.Position = request.Position;
            personnel.Date = request.Date;
            var master = _userService.GetUserByEmail(request.master);
            personnel.User = master;
            Personnel addedPersonnel =await _personnelService.AddPersonnel(personnel,user);
            string json = JsonConvert.SerializeObject(addedPersonnel, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }
        [HttpPost("GetPersonnelByUser")]
        public async Task<IActionResult> GetPersonnelByUser(PersonnelRequest request)
        {
            User user = _userService.GetUserByEmail(request.userEmail);
            string json = JsonConvert.SerializeObject( _personnelService.GetAllPersoneelByUser(user), Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Ok(json);

        }

        [HttpDelete("{personnelId}")]
        public async Task<string> DeletPersonnel(string personnelId)
        {
            if (personnelId == null) return null;
            var id = Int16.Parse(personnelId);
            return await _personnelService.DeletePersonnel(id);


        }

    }
}

