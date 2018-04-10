using System;
using System.Collections.Generic;
using System.Linq;
using DemoLab.Data.Access.Context.Interfaces;
using DemoLab.Data.Access.ExerciseExecutor;
using DemoLab.Services.ExercisePool;
using Moq;
using NUnit.Framework;

namespace DemoLab.Services.ExerciseManagement
{
    [TestFixture]
    internal class ExerciseServiceTests
    {
        private ExerciseService _service;
        private Mock<ICandidateExerciseContext> _exerciseContextMock;
        private Mock<IExercisePoolService> _exercisePoolMock;
        private Mock<ITestAssemblyContext> _testAssemblyContextMock;

        [SetUp]
        public void Init()
        {
            _exercisePoolMock = new Mock<IExercisePoolService>();
            _exerciseContextMock = new Mock<ICandidateExerciseContext>();
            _testAssemblyContextMock = new Mock<ITestAssemblyContext>();

            _service = new ExerciseService(
                _exerciseContextMock.Object,
                _exercisePoolMock.Object,
                _testAssemblyContextMock.Object);
        }

        [Test]
        public void Ctor_NullArgs_ThrowsException()
        {
            Assert.That(() => new ExerciseService(null, _exercisePoolMock.Object, _testAssemblyContextMock.Object), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => new ExerciseService(_exerciseContextMock.Object, null, _testAssemblyContextMock.Object), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => new ExerciseService(_exerciseContextMock.Object, _exercisePoolMock.Object, null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetItems_GuidEmptyArg_ThrowsException()
        {
            Assert.That(
                () => _service.GetCandidateExerciseList(Guid.Empty), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetItems_ZeroSet_ReturnsEmptyCollection()
        {
            // Arrange.
            var exercises = new DemoLab.Models.ExerciseManagement.CandidateExercise[] { };
            _exercisePoolMock.Setup(pool => pool.GetActiveExerciseSet()).Returns(exercises);

            var fakeTaskSet = new FakeEntitySet<CandidateTaskResult>(new List<CandidateTaskResult>());
            var fakeTestSet = new FakeEntitySet<CandidateTestResult>(new List<CandidateTestResult>());

            _exerciseContextMock.SetupGet(t => t.CandidateTaskResults).Returns(fakeTaskSet);
            _exerciseContextMock.SetupGet(t => t.CandidateTestResults).Returns(fakeTestSet);

            _service = new ExerciseService(
                _exerciseContextMock.Object,
                _exercisePoolMock.Object,
                _testAssemblyContextMock.Object);

            // Act.
            var result = _service.GetCandidateExerciseList(applicationUserId: Guid.NewGuid());

            // Assert.
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void GetItems_TwoItems_ReturnTwoItems()
        {
            // Arrange.
            var candidateTask = new DemoLab.Models.ExerciseManagement.CandidateTask { Id = 1 };
            var candidateTest = new DemoLab.Models.ExerciseManagement.CandidateTest { Id = 2 };
            var exercises = new DemoLab.Models.ExerciseManagement.CandidateExercise[] { candidateTask, candidateTest };

            _exercisePoolMock.Setup(pool => pool.GetActiveExerciseSet()).Returns(exercises);

            var candidateTaskResult = new CandidateTaskResult
            {
                CreatorId = Guid.Empty,
                CandidateExercise = new DemoLab.Data.Access.ExerciseManagement.CandidateTask { Id = 1 }
            };

            var candidateTestResult = new CandidateTestResult
            {
                CreatorId = Guid.Empty,
                CandidateExercise = new DemoLab.Data.Access.ExerciseManagement.CandidateExercise { Id = 2 }
            };

            var fakeTaskSet = new FakeEntitySet<CandidateTaskResult>(new List<CandidateTaskResult> { candidateTaskResult });
            var fakeTestSet = new FakeEntitySet<CandidateTestResult>(new List<CandidateTestResult> { candidateTestResult });

            _exerciseContextMock.SetupGet(t => t.CandidateTaskResults).Returns(fakeTaskSet);
            _exerciseContextMock.SetupGet(t => t.CandidateTestResults).Returns(fakeTestSet);

            _service = new ExerciseService(
                _exerciseContextMock.Object,
                _exercisePoolMock.Object,
                _testAssemblyContextMock.Object);

            // Act.
            var result = _service.GetCandidateExerciseList(applicationUserId: Guid.NewGuid());

            // Assert.
            Assert.AreEqual(2, result.Count());
        }
    }
}
