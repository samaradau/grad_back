using System.Collections.Generic;
using DemoLab.Models.ExerciseManagement;

namespace DemoLab.Services.ExercisePool
{
    /// <summary>
    /// Represents an exercise pool service contract.
    /// </summary>
    public interface IExercisePoolService
    {
        /// <summary>
        /// Returns a set of exercises that is assigned to the candidate by coach.
        /// </summary>
        /// <returns>Set of <see cref="CandidateExercise"/>.</returns>
        IEnumerable<CandidateExercise> GetActiveExerciseSet();

        /// <summary>
        /// Returns a task by id from active set.
        /// </summary>
        /// <param name="id">Identifier of the target task.</param>
        /// <returns>Instance of <see cref="CandidateTask"/>.</returns>
        CandidateTask GetCandidateTask(int id);
    }
}
