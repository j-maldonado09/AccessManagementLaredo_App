using AccessManagementLaredo_App.Models;
using Microsoft.AspNetCore.Identity;

namespace AccessManagementLaredo_App.Services
{
    public class IdentityServices
    {
        private UserManager<ApplicationUser> _userManager;
        private IHttpContextAccessor _contextAccessor;

        public IdentityServices(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApplicationUser> GetCurrentUserByName()
        {
            var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
            return user;
        }
    }
}
