using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoLab.Data.Access.ExerciseManagement
{
    /// <summary>
    /// Represents a test question.
    /// </summary>
    public class TestQuestion
    {
        /// <summary>
        /// Gets or sets an id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a candidate test to which the question belongs.
        /// </summary>
        public CandidateTest CandidateTest { get; set; }

        /// <summary>
        /// Gets or sets an candidate test id.
        /// </summary>
        [ForeignKey(nameof(CandidateTest))]
        public int CandidateTestId { get; set; }

        /// <summary>
        /// Gets or sets a full description of the question.
        /// </summary>
        [Required]
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or sets test answers.
        /// </summary>
        [Required]
        public virtual ICollection<TestAnswerVariant> Variants { get; set; }

        /// <summary>
        /// Gets or sets correct test answers.
        /// </summary>
        [Required]
        public virtual ICollection<TestAnswerVariant> CorrectVariants { get; set; }
    }
}
