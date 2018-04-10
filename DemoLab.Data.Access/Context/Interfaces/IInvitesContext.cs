using System.Threading.Tasks;
using DemoLab.Data.Access.UserManagement;

namespace DemoLab.Data.Access.Context.Interfaces
{
    /// <summary>
    /// Represents a context of invites.
    /// </summary>
    public interface IInvitesContext
    {
        /// <summary>
        /// Gets an invites set.
        /// </summary>
        IEntitySet<Invite> Invites { get; }

        /// <summary>
        /// Saves changes to database asynchronously.
        /// </summary>
        /// <returns>A task of saving all changes.</returns>
        Task SaveChangesAsync();
    }
}
