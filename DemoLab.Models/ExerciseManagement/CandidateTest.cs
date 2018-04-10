using System.Collections.Generic;

namespace DemoLab.Models.ExerciseManagement
{
    /// <summary>
    /// Represents an exam test.
    /// </summary>
    public class CandidateTest : CandidateExercise
    {
        /// <summary>
        /// Gets or sets a number of questions for a test.
        /// </summary>
        public IEnumerable<TestQuestion> Questions { get; set; }
    }
}