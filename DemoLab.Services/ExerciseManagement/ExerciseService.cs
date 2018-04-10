using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoLab.Data.Access.Context.Interfaces;
using DemoLab.Data.Access.ExerciseExecutor;
using DemoLab.Data.Access.ExerciseManagement;
using DemoLab.Models.ExerciseManagement;
using DemoLab.Services.Exceptions;
using DemoLab.Services.ExercisePool;
using CandidateExercise = DemoLab.Models.ExerciseManagement.CandidateExercise;
using CandidateTask = DemoLab.Models.ExerciseManagement.CandidateTask;
using CandidateTest = DemoLab.Models.ExerciseManagement.CandidateTest;

namespace DemoLab.Services.ExerciseManagement
{
    /// <summary>
    /// Represents an exercise service.
    /// </summary>
    internal class ExerciseService : IExerciseService
    {
        private const int ExerciseNotCompleted = -1;
        private const int TestUsedTipsNumber = 0;
        private readonly ICandidateExerciseContext _exerciseContext;
        private readonly IExercisePoolService _exercisePool;
        private readonly ITestAssemblyContext _assemblyContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExerciseService"/> class.
        /// </summary>
        /// <param name="exerciseContext">An instance of <see cref="ICandidateExerciseContext"/>.</param>
        /// <param name="exercisePool">An instance of <see cref="IExercisePoolService"/>.</param>
        /// <param name="assemblyContext">An instance of <see cref="ITestAssemblyContext"/>.</param>
        /// <exception cref="ArgumentNullException">Exception thrown when
        /// <paramref name="exerciseContext"/>, <paramref name="exercisePool"/>
        /// or <paramref name="assemblyContext"/> is null.</exception>
        public ExerciseService(
            ICandidateExerciseContext exerciseContext,
            IExercisePoolService exercisePool,
            ITestAssemblyContext assemblyContext)
        {
            _exerciseContext = exerciseContext ?? throw new ArgumentNullException(nameof(exerciseContext));
            _exercisePool = exercisePool ?? throw new ArgumentNullException(nameof(exercisePool));
            _assemblyContext = assemblyContext ?? throw new ArgumentNullException(nameof(assemblyContext));
        }

        public IEnumerable<ExerciseForList> GetCandidateExerciseList(Guid applicationUserId)
        {
            if (applicationUserId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(applicationUserId)} is an empty guid.", nameof(applicationUserId));
            }

            var exercises = _exercisePool.GetActiveExerciseSet();
            var candidateExerciseResults = GetCandidateExerciseResults(applicationUserId);

            var models = exercises.Select(exercise => CreateExerciseForList(exercise, candidateExerciseResults));
            return models.ToArray();
        }

        public CandidateTaskViewInfo GetCandidateTaskViewInfo(int id, Guid applicationUserId)
        {
            if (applicationUserId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(applicationUserId)} is an empty guid.", nameof(applicationUserId));
            }

            if (id < 1)
            {
                throw new ArgumentException($"{nameof(id)} is lower than 1.", nameof(id));
            }

            var candidateTask = _exercisePool.GetCandidateTask(id);
            var candidateExerciseResults = GetCandidateExerciseResults(applicationUserId);

            return CreateCandidateTask(candidateTask, candidateExerciseResults);
        }

        public IEnumerable<Tuple<string, int>> GetsCandidateExercisesNamesAndIds()
        {
            var taskData = _exerciseContext.CandidateTasks
                .Where(task => task.IsSoftDeleted == false)
                .Select(task => new { task.Name, task.Id })
                .AsEnumerable()
                .Select(taskInfo => Tuple.Create(taskInfo.Name, taskInfo.Id));

            var testsData = _exerciseContext.CandidateTests
                .Where(task => task.IsSoftDeleted == false)
                .Select(test => new { test.Name, test.Id })
                .AsEnumerable()
                .Select(testInfo => Tuple.Create(testInfo.Name, testInfo.Id));

            return taskData.Concat(testsData).ToArray();
        }

        public CandidateTaskInfo GetCandidateTaskInfo(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id must be greater than or equal to 1.", nameof(id));
            }

            var candidateTask = _exerciseContext.CandidateTasks.FirstOrDefault(task => task.Id == id);
            return candidateTask == null ? null :
                Mapper.Map<DemoLab.Data.Access.ExerciseManagement.CandidateTask, CandidateTaskInfo>(candidateTask);
        }

        public async Task<int> AddCandidateTaskAsync(CandidateTaskInfo candidateTaskInfo)
        {
            if (candidateTaskInfo == null)
            {
                throw new ArgumentNullException(nameof(candidateTaskInfo));
            }

            var candidateTask = CreateCandidateTask(candidateTaskInfo);
            _exerciseContext.CandidateTasks.Add(candidateTask);
            await _exerciseContext.SaveChangesAsync().ConfigureAwait(false);
            return candidateTask.Id;
        }

        public Task UpdateCandidateTaskAsync(CandidateTaskInfo candidateTaskInfo)
        {
            if (candidateTaskInfo == null)
            {
                throw new ArgumentNullException(nameof(candidateTaskInfo));
            }

            var candidateTask = _exerciseContext.CandidateTasks.FirstOrDefault(
                task => task.Id == candidateTaskInfo.Id);
            if (candidateTask == null)
            {
                throw new TaskNotFoundException($"Task with id = {candidateTaskInfo.Id} not found.");
            }

            UpdateCandidateTask(candidateTask, candidateTaskInfo);
            return _exerciseContext.SaveChangesAsync();
        }

        public Task DeleteCandidateExerciseAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id must be greater than or equal to 1.", nameof(id));
            }

            var candidateTask = _exerciseContext.CandidateTasks.FirstOrDefault(task => task.Id == id);
            if (candidateTask != null)
            {
                _exerciseContext.CandidateTasks.Remove(candidateTask);
                return _exerciseContext.SaveChangesAsync();
            }

            var candidateTest = _exerciseContext.CandidateTests.FirstOrDefault(task => task.Id == id);
            if (candidateTest == null)
            {
                throw new ExerciseNotFoundException($"Exercise with id = {id} not found.");
            }

            _exerciseContext.CandidateTests.Remove(candidateTest);
            return _exerciseContext.SaveChangesAsync();
        }

        public Task SoftDeleteCandidateExerciseAsync(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Id must be greater than or equal to 1.", nameof(id));
            }

            var candidateTask = _exerciseContext.CandidateTasks.FirstOrDefault(
                task => task.Id == id);
            if (candidateTask != null)
            {
                SoftDeleteCandidateExercise(candidateTask);
                return _exerciseContext.SaveChangesAsync();
            }

            var candidateTest = _exerciseContext.CandidateTests.FirstOrDefault(test => test.Id == id);
            if (candidateTest == null)
            {
                throw new ExerciseNotFoundException($"Exercise with id = {id} not found.");
            }

            SoftDeleteCandidateExercise(candidateTest);
            return _exerciseContext.SaveChangesAsync();
        }

        public bool IsResultAssociatedWithTaskExist(int candidateExerciseId)
        {
            var candidateTaskResult = _exerciseContext.CandidateTaskResults.FirstOrDefault(
                result => result.CandidateExerciseId == candidateExerciseId);
            var candidateTestResult = _exerciseContext.CandidateTestResults.FirstOrDefault(
                result => result.CandidateExerciseId == candidateExerciseId);
            return candidateTaskResult != null || candidateTestResult != null;
        }

        private static ExerciseForList CreateExerciseForList(
            CandidateExercise exercise,
            IList<CandidateExerciseResult> candidateExerciseResults)
        {
            return new ExerciseForList
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Subject = exercise.Subject,
                MaximumScore = exercise.MaximumScore,
                CandidateScore = candidateExerciseResults.FirstOrDefault(
                                     result => result.CandidateExerciseId == exercise.Id)?.Score ?? ExerciseNotCompleted,
                UsedTipsNumber = GetExerciseUsedTipsNumber(exercise, candidateExerciseResults),
                IsCompleted = candidateExerciseResults.Any(result => result.CandidateExerciseId == exercise.Id && result.Score != 0),
                ResultId = candidateExerciseResults.FirstOrDefault(
                               result => result.CandidateExerciseId == exercise.Id)?.Id ?? ExerciseNotCompleted,
                IsSoftDeleted = false,
                IsTimeOut = IsTimerOut(exercise, candidateExerciseResults),
                HasTimer = exercise.TimeSeconds.HasValue
            };
        }

        private static CandidateTaskViewInfo CreateCandidateTask(
            CandidateTask task,
            IList<CandidateExerciseResult> candidateExerciseResults)
        {
            return new CandidateTaskViewInfo
            {
                Id = task.Id,
                Name = task.Name,
                Subject = task.Subject,
                Description = task.Description,
                MaximumScore = task.MaximumScore,
                TimeSeconds = task.TimeSeconds,
                CodeTemplate = task.CodeTemplate,
                Tips = task.Tips.ToArray(),
                CandidateScore = candidateExerciseResults.FirstOrDefault(
                                     result => result.CandidateExerciseId == task.Id)?.Score ?? ExerciseNotCompleted,
                UsedTipsNumber = GetExerciseUsedTipsNumber(task, candidateExerciseResults),
                IsCompleted = candidateExerciseResults.Any(result => result.CandidateExerciseId == task.Id && result.Score != 0),
                IsSoftDeleted = false
            };
        }

        private static int GetExerciseUsedTipsNumber(
            CandidateExercise exercise,
            IEnumerable<CandidateExerciseResult> candidateExerciseResults)
        {
            if (exercise.GetType() == typeof(CandidateTest))
            {
                return TestUsedTipsNumber;
            }

            var exerciseResult = candidateExerciseResults.FirstOrDefault(
                result => result.CandidateExerciseId == exercise.Id);

            if (exerciseResult == null)
            {
                return TestUsedTipsNumber;
            }

            var taskResult = exerciseResult as CandidateTaskResult;
            return taskResult?.UsedTipsNumber ?? TestUsedTipsNumber;
        }

        private static bool IsTimerOut(CandidateExercise task, IList<CandidateExerciseResult> candidateExerciseResults)
        {
            if (task == null || candidateExerciseResults == null)
            {
                return false;
            }

            var result = candidateExerciseResults.FirstOrDefault(res => res.CandidateExerciseId == task.Id);

            if (result == null || result.Score != 0 || !(result is CandidateTaskResult taskResult)
                || taskResult.StartDate == null)
            {
                return false;
            }

            return (DateTime.UtcNow - taskResult.StartDate.Value).TotalSeconds > task.TimeSeconds;
        }

        private IList<CandidateExerciseResult> GetCandidateExerciseResults(Guid applicationUserId)
        {
            var candidateExerciseResults = new List<CandidateExerciseResult>();

            var candidateTaskResults = _exerciseContext.CandidateTaskResults
                .Where(result => result.CreatorId == applicationUserId)
                .ToArray();

            var candidateTestResults = _exerciseContext.CandidateTestResults
                .Where(result => result.CreatorId == applicationUserId)
                .ToArray();

            candidateExerciseResults.AddRange(candidateTaskResults);
            candidateExerciseResults.AddRange(candidateTestResults);

            return candidateExerciseResults;
        }

        private DemoLab.Data.Access.ExerciseManagement.CandidateTask CreateCandidateTask(
            CandidateTaskInfo candidateTaskInfo)
        {
            var result = new DemoLab.Data.Access.ExerciseManagement.CandidateTask
            {
                CodeTemplate = candidateTaskInfo.CodeTemplate,
                Description = candidateTaskInfo.Description,
                MaximumScore = candidateTaskInfo.MaximumScore,
                TimeSeconds = candidateTaskInfo.TimeSeconds,
                Name = candidateTaskInfo.Name,
                Subject = candidateTaskInfo.Subject,
                IsSoftDeleted = false
            };

            if (candidateTaskInfo.Tips != null)
            {
                result.Tips = candidateTaskInfo.Tips.Select(
                    tipText => new CandidateTaskTip { Text = tipText }).ToList();
            }

            SetTestClassAndMethod(result, candidateTaskInfo);

            return result;
        }

        private void UpdateCandidateTask(
            DemoLab.Data.Access.ExerciseManagement.CandidateTask candidateTask,
            CandidateTaskInfo candidateTaskInfo)
        {
            candidateTask.CodeTemplate = candidateTaskInfo.CodeTemplate;
            candidateTask.Description = candidateTaskInfo.Description;
            candidateTask.MaximumScore = candidateTaskInfo.MaximumScore;
            candidateTask.TimeSeconds = candidateTaskInfo.TimeSeconds;
            candidateTask.Name = candidateTaskInfo.Name;
            candidateTask.Subject = candidateTaskInfo.Subject;

            if (candidateTaskInfo.Tips != null)
            {
                UpdateTips(candidateTask.Tips, candidateTaskInfo.Tips.ToArray());
            }
            else
            {
                UpdateTips(candidateTask.Tips);
            }

            SetTestClassAndMethod(candidateTask, candidateTaskInfo);
        }

        private void SoftDeleteCandidateExercise(
            DemoLab.Data.Access.ExerciseManagement.CandidateExercise candidateExercise)
        {
            candidateExercise.IsSoftDeleted = true;
        }

        private void SetTestClassAndMethod(
            DemoLab.Data.Access.ExerciseManagement.CandidateTask candidateTask,
            CandidateTaskInfo candidateTaskInfo)
        {
            var testClass = _assemblyContext.TestClassInfo.FirstOrDefault(classInfo =>
                classInfo.Name.Equals(candidateTaskInfo.TestClassName, StringComparison.Ordinal));

            candidateTask.TestClassId = testClass?.Id ?? throw new TestClassInfoNotFoundException();

            if (!string.IsNullOrWhiteSpace(candidateTaskInfo.TestMethodName))
            {
                var testMethod = testClass.TestMethods.FirstOrDefault(method =>
                    method.Name.Equals(candidateTaskInfo.TestMethodName, StringComparison.Ordinal));
                candidateTask.TestMethodId = testMethod?.Id;
            }
        }

        private void UpdateTips(ICollection<CandidateTaskTip> oldTips, IReadOnlyList<string> newTips = null)
        {
            int i;
            if (newTips == null)
            {
                i = oldTips.Count;
                while (i > 0)
                {
                    var tip = oldTips.ElementAt(--i);
                    _exerciseContext.CandidateTaskTips.Remove(tip);
                    oldTips.Remove(tip);
                }
            }
            else
            {
                for (i = newTips.Count; i < oldTips.Count; i++)
                {
                    var tip = oldTips.ElementAt(i--);
                    _exerciseContext.CandidateTaskTips.Remove(tip);
                    oldTips.Remove(tip);
                }

                i = 0;
                foreach (var oldTip in oldTips)
                {
                    oldTip.Text = newTips[i++];
                }

                for (i = oldTips.Count; i < newTips.Count; i++)
                {
                    oldTips.Add(new CandidateTaskTip { Text = newTips[i] });
                }
            }
        }
    }
}
