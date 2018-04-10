using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DemoLab.Data.Access.Context.Interfaces;
using DemoLab.Data.Access.ExerciseExecutor;
using Moq;
using NUnit.Framework;

namespace DemoLab.Services.ExerciseExecutor
{
    [TestFixture]
    public class CandidateExerciseResultServiceTests
    {
        private readonly Guid _candidateId = Guid.NewGuid();
        private CandidateExercisesResultsService _service;
        private IList<CandidateTaskResult> _taskResults = new List<CandidateTaskResult>();
        private IList<CandidateTestResult> _testResults = new List<CandidateTestResult>();

        [OneTimeSetUp]
        public void Init()
        {
            var fakeTaskResults = new FakeEntitySet<CandidateTaskResult>(_taskResults);
            var fakeTestResults = new FakeEntitySet<CandidateTestResult>(_testResults);
            var context = Mock.Of<ICandidateExerciseContext>(c => c.CandidateTaskResults == fakeTaskResults && c.CandidateTestResults == fakeTestResults);
            _service = new CandidateExercisesResultsService(context);
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(Mapping.CandidateExerciseMappingProfile)));
        }

        [TearDown]
        public void Cleanup()
        {
            _taskResults.Clear();
            _testResults.Clear();
        }

        [Test]
        public void GetCandidateExercisesResults_CandidateId_ReturnsAllCandidatesTasksResults()
        {
            var result1 = new CandidateTaskResult() { Id = 1, CreatorId = _candidateId, IsCompleted = true };
            var result2 = new CandidateTaskResult() { Id = 2, CreatorId = Guid.NewGuid(), IsCompleted = true };
            var result3 = new CandidateTaskResult() { Id = 3, CreatorId = _candidateId, IsCompleted = true };
            _taskResults.Add(result1);
            _taskResults.Add(result2);
            _taskResults.Add(result3);

            var results = _service.GetCandidateExercisesResults(_candidateId).ToArray();

            CollectionAssert.AreEquivalent(new[] { result1.Id, result3.Id }, results.Select(r => r.Id));
        }

        [Test]
        public void GetCandidateExercisesResults_CandidateId_ReturnsAllCompletedTasksResults()
        {
            var result1 = new CandidateTaskResult() { Id = 1, CreatorId = _candidateId, IsCompleted = true };
            var result2 = new CandidateTaskResult() { Id = 2, CreatorId = _candidateId, IsCompleted = true };
            var result3 = new CandidateTaskResult() { Id = 3, CreatorId = _candidateId, IsCompleted = false };
            _taskResults.Add(result1);
            _taskResults.Add(result2);
            _taskResults.Add(result3);

            var results = _service.GetCandidateExercisesResults(_candidateId).ToArray();

            CollectionAssert.AreEquivalent(new[] { result1.Id, result2.Id }, results.Select(r => r.Id));
        }

        [Test]
        public void GetCandidateExercisesResults_CandidateId_ReturnsAllCandidatesTestsResults()
        {
            var result1 = new CandidateTestResult() { Id = 1, CreatorId = Guid.NewGuid() };
            var result2 = new CandidateTestResult() { Id = 2, CreatorId = _candidateId };
            var result3 = new CandidateTestResult() { Id = 3, CreatorId = _candidateId };
            _testResults.Add(result1);
            _testResults.Add(result2);
            _testResults.Add(result3);

            var results = _service.GetCandidateExercisesResults(_candidateId).ToArray();

            CollectionAssert.AreEquivalent(new[] { result2.Id, result3.Id }, results.Select(r => r.Id));
        }

        [Test]
        public void GetCandidateExercisesResults_CandidateId_ReturnsOrderedByScoreDescending()
        {
            var result1 = new CandidateTestResult() { Id = 1, CreatorId = _candidateId, Score = 3 };
            var result2 = new CandidateTestResult() { Id = 2, CreatorId = _candidateId, Score = 0 };
            var result3 = new CandidateTaskResult() { Id = 3, CreatorId = _candidateId, IsCompleted = true, Score = 9 };
            var result4 = new CandidateTaskResult() { Id = 4, CreatorId = _candidateId, IsCompleted = true, Score = 4 };
            _testResults.Add(result1);
            _testResults.Add(result2);
            _taskResults.Add(result3);
            _taskResults.Add(result4);

            var results = _service.GetCandidateExercisesResults(_candidateId).ToArray();

            Assert.That(results, Is.Ordered.Descending.By("Score"));
        }
    }
}
