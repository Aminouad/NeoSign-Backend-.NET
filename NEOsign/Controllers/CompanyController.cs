using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEOsign.Model;
using Newtonsoft.Json;

namespace NEOsign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserService _userService;
        private DataContext _context;
        private readonly ICompanyService _companyService;

        public CompanyController(IConfiguration configuration, IUserService userService, DataContext context, ICompanyService companyService)
        {
            _configuration = configuration;
            _userService = userService;
            _context = context;
            _companyService = companyService;
        }
        [HttpPost("register"), Authorize]
        public async Task<ActionResult<User>> Register(CompanyDto request)
        {
            var user = new User();       
            AuthenticationController.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Email = request.Contact;
            user.Role = request.Role;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var company = new Company();
            company.Name = request.Name;
            company.Contact = request.Contact;
            company.Phone = request.Phone;
            company.Address = request.Address;
            company.Etat = request.Etat;
            company.Date = request.Date;
            var master = _userService.GetUserByEmail(request.master);
            company.User = master;          
            await _companyService.AddCompany(company, user);
            string json = JsonConvert.SerializeObject(company, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {


            string json = JsonConvert.SerializeObject(await _companyService.GetAllCompany(), Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Ok(json);

        }

        [HttpDelete("{companyId}") ]
        public async Task<string> DeleteCompany(string companyId)
        {
            if (companyId == null) return null;
            var id = Int16.Parse(companyId);
            return await _companyService.DeleteCompany(id);


        }
    }
}

