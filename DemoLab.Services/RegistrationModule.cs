using DemoLab.Services.ExerciseExecutor;
using DemoLab.Services.ExerciseManagement;
using DemoLab.Services.UserManagement;
using Ninject.Modules;

namespace DemoLab.Services
{
    /// <summary>
    /// Represents a registration module for NInject.
    /// </summary>
    public class RegistrationModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ICandidateTaskEvaluationService>().To<CandidateTaskEvaluationService>();
            Bind<ICandidateExercisesResultsService>().To<CandidateExercisesResultsService>();
            Bind<ICandidateTaskRunner>().To<CandidateTaskRunner>();
            Bind<ITestAssemblyService>().To<TestAssemblyService>();
            Bind<IInvitesService>().To<InvitesService>();
            Bind<IEmailSendingService>().To<EmailSendingService>();
            Bind<IExerciseService>().To<ExerciseService>();
            Bind<ExercisePool.IExercisePoolService>().To<ExercisePool.ExercisePoolService>();
        }
    }
}
