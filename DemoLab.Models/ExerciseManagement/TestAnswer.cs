namespace DemoLab.Models.ExerciseManagement
{
    /// <summary>
    /// Represents an answer to a test question.
    /// </summary>
    public class TestAnswer
    {
        /// <summary>
        /// Gets or sets id of an answer.
        /// </summary>
        public int AnswerId { get; set; }

        /// <summary>
        /// Gets or sets a question to which this answer belongs.
        /// </summary>
        public TestQuestion Question { get; set; }

        /// <summary>
        /// Gets or sets the id of the question to which this answer belongs.
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets a text of the answer.
        /// </summary>
        public string AnswerText { get; set; }

        public bool IsChecked { get; set; }
    }
}