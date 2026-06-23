using AuthLab.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Services
{
    public class IdentityService
    {
        private readonly UserManager<User> _userManager;

        public IdentityService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    }
}
