using System.Threading.Tasks;
using DemoLab.Data.Access.ExerciseExecutor;
using DemoLab.Data.Access.ExerciseManagement;

namespace DemoLab.Data.Access.Context.Interfaces
{
    /// <summary>
    /// Represents an exercise context.
    /// </summary>
    public interface ICandidateExerciseContext
    {
        /// <summary>
        /// Gets a candidate tasks set.
        /// </summary>
        IEntitySet<CandidateTask> CandidateTasks { get; }

        /// <summary>
        /// Gets a candidate tests set.
        /// </summary>
        IEntitySet<CandidateTest> CandidateTests { get; }

        /// <summary>
        /// Gets a candidate tasks results set.
        /// </summary>
        IEntitySet<CandidateTaskResult> CandidateTaskResults { get; }

        /// <summary>
        /// Gets a candidate tests results set.
        /// </summary>
        IEntitySet<CandidateTestResult> CandidateTestResults { get; }

        /// <summary>
        /// Gets a candidate tasks tips set.
        /// </summary>
        IEntitySet<CandidateTaskTip> CandidateTaskTips { get; }

        /// <summary>
        /// Saves all changes made in this context to an underlying storage.
        /// </summary>
        /// <returns>A task result of saving to a data base.</returns>
        Task SaveChangesAsync();
    }
}
