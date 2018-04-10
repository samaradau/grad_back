using System.Threading.Tasks;

namespace DemoLab.Services.Security
{
    public interface IUserContextService
    {
        IUserContext GetCurrentUser();

        Task<IUserContext> GetCurrentUserAsync();
    }
}
