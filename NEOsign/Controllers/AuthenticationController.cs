global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;

global using NEOsign.Model;

namespace NEOsign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly DataContext _context;
        public static User user = new User();

        public AuthenticationController(IConfiguration configuration, IUserService userService, DataContext context)
        {
            _configuration = configuration;
            _userService = userService;
            _context = context;
        }
        [HttpGet, Authorize]
        public ActionResult<object> GetUserIdentity()
        {
            //var userName = User?.Identity?.Name;
            //var userName2 = User.FindFirstValue(ClaimTypes.Name);
            //var role = User.FindFirstValue(ClaimTypes.Role);

            //return Ok(new { userName,userName2,role});
            var userInfo = _userService.GetUserInfo();
            return Ok(userInfo);

        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDtoRegister request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Email = request.Email;
            user.Role = request.Role;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDtoLogin request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Email == request.Email);

            if (user == null) 
            {
                return Ok("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Ok("Wrong password.");
            }

            string token = CreateToken(user);
            return Ok(token);

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Id.ToString()),

                new Claim(ClaimTypes.Role, user.Role),
               

            };
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);



            return jwt;
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash,
                out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {

                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }


        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))

            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }



    }
}
