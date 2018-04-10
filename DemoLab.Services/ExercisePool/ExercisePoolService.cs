using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DemoLab.Data.Access.Context.Interfaces;
using DemoLab.Models.ExerciseManagement;

namespace DemoLab.Services.ExercisePool
{
    /// <summary>
    /// Represents an exercise pool service.
    /// </summary>
    internal class ExercisePoolService : IExercisePoolService
    {
        private readonly ICandidateExerciseContext _exerciseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExercisePoolService"/> class.
        /// </summary>
        /// <param name="exerciseContext">An instance of <see cref="ICandidateExerciseContext"/>.</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="exerciseContext"/> is null.</exception>
        public ExercisePoolService(ICandidateExerciseContext exerciseContext)
        {
            _exerciseContext = exerciseContext ?? throw new ArgumentNullException(nameof(exerciseContext));
        }

        public IEnumerable<CandidateExercise> GetActiveExerciseSet()
        {
            var entityTasks = _exerciseContext.CandidateTasks.Where(task => !task.IsSoftDeleted).ToArray();

            var modelTasks = Mapper.Map<IEnumerable<DemoLab.Data.Access.ExerciseManagement.CandidateTask>,
                IEnumerable<CandidateTask>>(entityTasks);

            var result = new List<CandidateExercise>();
            result.AddRange(modelTasks);

            return result;
        }

        public CandidateTask GetCandidateTask(int id)
        {
            var task = _exerciseContext.CandidateTasks.FirstOrDefault(t => t.Id == id);

            return task == null ? null
                : Mapper.Map<DemoLab.Data.Access.ExerciseManagement.CandidateTask, CandidateTask>(task);
        }
    }
}