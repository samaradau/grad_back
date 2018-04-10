using System.Collections.Generic;
using DemoLab.Data.Access.ExerciseExecutor;

namespace DemoLab.Data.Access.ExerciseManagement
{
    /// <summary>
    /// Represents an exam task.
    /// </summary>
    public class CandidateTask : CandidateExercise
    {
        /// <summary>
        /// Gets or sets a code template.
        /// </summary>
        public string CodeTemplate { get; set; }

        /// <summary>
        /// Gets or sets a test method.
        /// </summary>
        public virtual TestMethodInfo TestMethod { get; set; }

        /// <summary>
        /// Gets or sets a test method id.
        /// </summary>
        public int? TestMethodId { get; set; }

        /// <summary>
        /// Gets or sets a test class.
        /// </summary>
        public virtual TestClassInfo TestClass { get; set; }

        /// <summary>
        /// Gets or sets a test class id.
        /// </summary>
        public int TestClassId { get; set; }

        /// <summary>
        /// Gets or sets tips for a particular exercise.
        /// </summary>
        public virtual ICollection<CandidateTaskTip> Tips { get; set; } = new List<CandidateTaskTip>();
    }
}
