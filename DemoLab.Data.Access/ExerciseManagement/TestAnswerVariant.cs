using System.ComponentModel.DataAnnotations;

namespace DemoLab.Data.Access.ExerciseManagement
{
    /// <summary>
    /// Represents a variant of an answer to the test question.
    /// </summary>
    public class TestAnswerVariant
    {
        /// <summary>
        /// Gets or sets an id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a text of an answer.
        /// </summary>
        [Required]
        public string Text { get; set; }
    }
}
