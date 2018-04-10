using System.Collections.Generic;

namespace DemoLab.Data.Access.ExerciseManagement
{
    /// <summary>
    /// Represents an exam candidate test.
    /// </summary>
    public class CandidateTest : CandidateExercise
    {
        /// <summary>
        /// Gets or sets test questions.
        /// </summary>
        public IEnumerable<TestQuestion> Questions { get; set; }
    }
}
