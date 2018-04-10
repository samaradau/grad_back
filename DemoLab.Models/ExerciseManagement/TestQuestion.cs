using System.Collections.Generic;

namespace DemoLab.Models.ExerciseManagement
{
    /// <summary>
    /// Represents a question in the test.
    /// </summary>
    public class TestQuestion
    {
        /// <summary>
        /// Gets or sets id of the question.
        /// </summary>
        public int QuestionId { get; set; }

        public CandidateTest CandidateTest { get; set; }

        public int CandidateTestId { get; set; }

        /// <summary>
        /// Gets or sets full description of the question.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets all variants of answer to this question.
        /// </summary>
        public IEnumerable<TestAnswer> Variants { get; set; }

        /// <summary>
        /// Gets or sets only correct variants of answer to this question.
        /// </summary>
        public IEnumerable<TestAnswer> CorrectVariants { get; set; }

        public bool IsComplete { get; set; }
    }
}