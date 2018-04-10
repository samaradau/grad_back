using System;
using System.Threading.Tasks;
using DemoLab.Data.Access.Context.Interfaces;
using DemoLab.Data.Access.ExerciseExecutor;
using DemoLab.Data.Access.ExerciseManagement;

namespace DemoLab.Data.Access.Context
{
    internal class CandidateExerciseContext : ICandidateExerciseContext
    {
        private readonly ApplicationDbContext _context;

        public CandidateExerciseContext(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            CandidateTasks = new EntitySet<CandidateTask>(_context.CandidateTasks);
            CandidateTests = new EntitySet<CandidateTest>(_context.CandidateTests);
            CandidateTaskResults = new EntitySet<CandidateTaskResult>(_context.CandidateTaskResults);
            CandidateTestResults = new EntitySet<CandidateTestResult>(_context.CandidateTestResults);
            CandidateTaskTips = new EntitySet<CandidateTaskTip>(_context.CandidateTaskTips);
        }

        public IEntitySet<CandidateTask> CandidateTasks { get; }

        public IEntitySet<CandidateTest> CandidateTests { get; }

        public IEntitySet<CandidateTaskResult> CandidateTaskResults { get; }

        public IEntitySet<CandidateTestResult> CandidateTestResults { get; }

        public IEntitySet<CandidateTaskTip> CandidateTaskTips { get; }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
