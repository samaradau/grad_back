using System;
using DemoLab.Models.ExerciseExecutor;
using DemoLab.Models.ExerciseManagement;
using DemoLab.Runner;

namespace DemoLab.Services.ExerciseExecutor
{
    internal class CandidateTaskRunner : ICandidateTaskRunner
    {
        private readonly INUnitFilterFactory _nUnitFilterFactory;

        public CandidateTaskRunner(INUnitFilterFactory nUnitFilterFactory)
        {
            _nUnitFilterFactory = nUnitFilterFactory ?? throw new ArgumentNullException(nameof(nUnitFilterFactory));
        }

        public TaskRunResult Run(CandidateTask task, string code)
        {
            AppDomain domain = null;

            try
            {
                domain = AppDomain.CreateDomain(
                    $"domain_{Guid.NewGuid().ToString()}",
                    AppDomain.CurrentDomain.Evidence,
                    AppDomain.CurrentDomain.SetupInformation);

                var runner = (UserTestRunner)domain.CreateInstanceAndUnwrap(
                    typeof(UserTestRunner).Assembly.FullName,
                    typeof(UserTestRunner).FullName,
                    true,
                    System.Reflection.BindingFlags.CreateInstance,
                    null,
                    new object[] { _nUnitFilterFactory },
                    null,
                    null);

                if (!runner.Run(task.TestClass.AssemblyInfo.Data, code, task.TestClass.Name, task.TestMethod?.Name))
                {
                    return new TaskRunResult
                    {
                        Success = false,
                        ErrorMessage = runner.ErrorMessage
                    };
                }

                return new TaskRunResult { Success = true };
            }
            finally
            {
                if (domain != null)
                {
                    AppDomain.Unload(domain);
                }
            }
        }
    }
}
