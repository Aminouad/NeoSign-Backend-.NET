using NEOsign.Repositories;
using System.Security.Claims;

namespace NEOsign.Services
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;

        }

        public object GetUserInfo()
        {

            var email = string.Empty;
            var role = string.Empty;
            var userId = string.Empty;


            if (_httpContextAccessor.HttpContext != null)
            {
                userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
                role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                 
            }

            return new { userId, email, role };

        }
        public User GetUserByEmail(string email)
        {
             var user = _userRepository.GetUserByEmail(email);
            return user;
        }

    }
}
