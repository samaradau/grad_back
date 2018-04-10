using System.Threading.Tasks;
using System.Web;
using DemoLab.Services.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DemoLab.Services.Security
{
    internal class UserContextService : IUserContextService
    {
        private UserContext _userContext;

        public IUserContext GetCurrentUser()
        {
            if (_userContext == null)
            {
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = userManager.FindById(HttpContext.Current.User.Identity.GetUserId());
                _userContext = new UserContext(user);
            }

            return _userContext;
        }

        public async Task<IUserContext> GetCurrentUserAsync()
        {
            if (_userContext == null)
            {
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = await userManager.FindByIdAsync(HttpContext.Current.User.Identity.GetUserId());
                _userContext = new UserContext(user);
            }

            return _userContext;
        }
    }
}
