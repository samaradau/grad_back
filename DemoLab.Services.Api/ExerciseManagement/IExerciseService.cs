﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoLab.Models.ExerciseManagement;
using DemoLab.Services.Exceptions;

namespace DemoLab.Services.ExerciseManagement
{
    /// <summary>
    /// Represents an exercise service contract.
    /// </summary>
    public interface IExerciseService
    {
        /// <summary>
        /// Returns a list of exercises that is assigned to the candidate
        /// by coach with his results.
        /// </summary>
        /// <param name="applicationUserId">Application user id.</param>
        /// <returns>List of <see cref="ExerciseForList"/>.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="applicationUserId"/> is an empty guid.</exception>
        IEnumerable<ExerciseForList> GetCandidateExerciseList(Guid applicationUserId);

        /// <summary>
        /// Returns a candidate task by id from the list
        /// that is assigned to the candidate by coach with his results.
        /// </summary>
        /// <param name="id">Identifier of the target candidate task.</param>
        /// <param name="applicationUserId">Application user identifier.</param>
        /// <returns>Instance of <see cref="CandidateTaskViewInfo"/>.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="applicationUserId"/> is an empty guid or when
        /// <paramref name="id"/> is an invalid id.</exception>
        CandidateTaskViewInfo GetCandidateTaskViewInfo(int id, Guid applicationUserId);

        /// <summary>
        /// Returns a list of exercises names and ids.
        /// </summary>
        /// <remarks>
        /// Item1 is the name of the exercise, item2 is the id of the exercise.
        /// </remarks>
        /// <returns>List of exercises names and ids.</returns>
        IEnumerable<Tuple<string, int>> GetsCandidateExercisesNamesAndIds();

        /// <summary>
        /// Retuns a candidate task info identifying by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Identifier of the target candidate task.</param>
        /// <returns>A candidate task info.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="id"/> is less than 1.</exception>
        CandidateTaskInfo GetCandidateTaskInfo(int id);

        /// <summary>
        /// Adds a candidate task.
        /// </summary>
        /// <param name="candidateTaskInfo">Information about a candidate task.</param>
        /// <returns>Created task id.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="candidateTaskInfo"/> is null.</exception>
        /// <exception cref="TestClassInfoNotFoundException">Exception thrown when
        /// <code>CandidateTaskInfo.TestClassName</code> can not be found.</exception>
        Task<int> AddCandidateTaskAsync(CandidateTaskInfo candidateTaskInfo);

        /// <summary>
        /// Updates a task identifying by <code>CandidateTaskInfo.Id</code>
        /// using data from the <paramref name="candidateTaskInfo"/>.
        /// </summary>
        /// <param name="candidateTaskInfo">New task info.</param>
        /// <returns>An updating operation result.</returns>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="candidateTaskInfo"/> is null.</exception>
        /// <exception cref="TestClassInfoNotFoundException">Exception thrown when
        /// class info identifying by <code>CandidateTaskInfo.TestClassName</code>
        /// can not be found.</exception>
        /// <exception cref="TaskNotFoundException">Exception thrown when
        /// task identifying by <code>CandidateTaskInfo.Id</code> does not exist.</exception>
        Task UpdateCandidateTaskAsync(CandidateTaskInfo candidateTaskInfo);

        /// <summary>
        /// Deletes a candidate exercise identifying by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">A candidate exercise id.</param>
        /// <returns>A deletion operation result.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="id"/> is less than 1.</exception>
        /// <exception cref="ExerciseNotFoundException">Exception thrown when
        /// exercise identifying by <code>CandidateTaskInfo.Id</code> does not exist.</exception>
        Task DeleteCandidateExerciseAsync(int id);

        /// <summary>
        /// Softly deletes a candidate exercise identifying by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">A candidate exercise id.</param>
        /// <returns>A deletion operation result.</returns>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="id"/> is less than 1.</exception>
        /// <exception cref="ExerciseNotFoundException">Exception thrown when
        /// exercise identifying by <code>CandidateTaskInfo.Id</code> does not exist.</exception>
        Task SoftDeleteCandidateExerciseAsync(int id);

        /// <summary>
        /// Checks whether the result is related to the task.
        /// </summary>
        /// <param name="candidateExerciseId">A candidate exercise id.</param>
        /// <returns>Value indicating whether result is associated with exercise.</returns>
        bool IsResultAssociatedWithTaskExist(int candidateExerciseId);
    }
}