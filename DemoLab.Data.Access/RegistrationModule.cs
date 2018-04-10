using DemoLab.Data.Access.Context;
using DemoLab.Data.Access.Context.Interfaces;
using Ninject.Modules;

namespace DemoLab.Data.Access
{
    /// <summary>
    /// Represents a registration module for Ninject.
    /// </summary>
    public class RegistrationModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ApplicationDbContext>().To<ApplicationDbContext>();
            Bind<IInvitesContext>().To<InvitesContext>();
            Bind<ICandidateExerciseContext>().To<CandidateExerciseContext>();
            Bind<ITestAssemblyContext>().To<TestAssemblyContext>();
        }
    }
}
