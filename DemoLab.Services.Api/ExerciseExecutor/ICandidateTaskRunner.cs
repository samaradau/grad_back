using DemoLab.Models.ExerciseExecutor;
using DemoLab.Models.ExerciseManagement;

namespace DemoLab.Services.ExerciseExecutor
{
    /// <summary>
    /// Represents a candidate task runner contract.
    /// </summary>
    public interface ICandidateTaskRunner
    {
        /// <summary>
        /// Runs candidate's task.
        /// </summary>
        /// <param name="task">Candidate's task.</param>
        /// <param name="code">Code template.</param>
        /// <returns>Result of task run.</returns>
        TaskRunResult Run(CandidateTask task, string code);
    }
}
