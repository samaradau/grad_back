using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoLab.Data.Access.Context.Interfaces;
using DemoLab.Data.Access.ExerciseExecutor;
using DemoLab.Services.Exceptions;

namespace DemoLab.Services.ExerciseExecutor
{
    /// <summary>
    /// Represents a candidate exercises results service.
    /// </summary>
    internal class CandidateExercisesResultsService : ICandidateExercisesResultsService
    {
        private readonly ICandidateExerciseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateExercisesResultsService"/> class.
        /// </summary>
        /// <param name="context">An instance of <see cref="ICandidateExerciseContext"/>.</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="context"/>is null.</exception>
        public CandidateExercisesResultsService(ICandidateExerciseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Returns all candidate's task results.
        /// </summary>
        /// <param name="candidateId">Candidate's domain id.</param>
        /// <returns>All task results.</returns>
        public IEnumerable<Models.ExerciseExecutor.CandidateTaskResult> GetTaskResults(Guid candidateId)
        {
            var taskResults = _context.CandidateTaskResults.ToArray();
            return Mapper.Map<IEnumerable<DemoLab.Models.ExerciseExecutor.CandidateTaskResult>>(taskResults).Where(r => r.CreatorId == candidateId);
        }

        /// <summary>
        /// Finds a task result by id.
        /// </summary>
        /// <param name="resultId">An id of a task's result.</param>
        /// /// <param name="candidateId">A candidate id.</param>
        /// <returns>An instance of <see cref="CandidateTaskResult"/>.</returns>
        public DemoLab.Models.ExerciseExecutor.CandidateTaskResult GetTaskResultById(int resultId, Guid candidateId)
        {
            VerifyId(resultId);

            return Mapper.Map<DemoLab.Models.ExerciseExecutor.CandidateTaskResult>(
                _context.CandidateTaskResults.FirstOrDefault(i => i.Id == resultId && i.CreatorId == candidateId));
        }

        /// <summary>
        /// Finds a task result by task id and candidate id.
        /// </summary>
        /// <param name="taskId">An id of a task.</param>
        /// <param name="candidateId">A candidate id.</param>
        /// <returns>An instance of <see cref="CandidateTaskResult"/>.</returns>
        public DemoLab.Models.ExerciseExecutor.CandidateTaskResult GetTaskResultByTaskId(int taskId, Guid candidateId)
        {
            VerifyId(taskId);

            return Mapper.Map<DemoLab.Models.ExerciseExecutor.CandidateTaskResult>(
                _context.CandidateTaskResults.FirstOrDefault(i => i.CandidateExerciseId == taskId && i.CreatorId == candidateId));
        }

        /// <summary>
        /// Removes a task result by id.
        /// </summary>
        /// <param name="id">An id of a task result.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task RemoveTaskResultAsync(int id)
        {
            VerifyId(id);

            var temp = _context.CandidateTaskResults.FirstOrDefault(cr => cr.Id == id);
            if (temp == null)
            {
                throw new TaskResultNotFoundException();
            }

            _context.CandidateTaskResults.Remove(
                Mapper.Map<CandidateTaskResult>(temp));
            return _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets candidate's completed exercises results.
        /// </summary>
        /// <param name="candidateId">Candidate's domain id.</param>
        /// <returns>Candidate's completed exercises results.</returns>
        public IEnumerable<Models.ExerciseExecutor.CandidateExerciseResult> GetCandidateExercisesResults(Guid candidateId)
        {
            return Mapper.Map<IEnumerable<Models.ExerciseExecutor.CandidateExerciseResult>>(
                _context.CandidateTaskResults
                .Where(r => r.CreatorId == candidateId && r.IsCompleted)
                .Cast<CandidateExerciseResult>()
                .Concat(_context.CandidateTestResults.Where(r => r.CreatorId == candidateId))
                .OrderByDescending(r => r.Score));
        }

        private void VerifyId(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException($"{nameof(id)} must be greater than 1.", nameof(id));
            }
        }
    }
}