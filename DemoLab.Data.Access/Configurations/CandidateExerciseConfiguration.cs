using System.Data.Entity.ModelConfiguration;
using DemoLab.Data.Access.ExerciseManagement;

namespace DemoLab.Data.Access.Configurations
{
    /// <summary>
    /// Represents an invite table configuration for Entity Framework.
    /// </summary>
    internal sealed class CandidateExerciseConfiguration : EntityTypeConfiguration<CandidateExercise>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateExerciseConfiguration"/> class.
        /// </summary>
        public CandidateExerciseConfiguration()
        {
            ToTable("CandidateExercises");
            HasKey<int>(i => i.Id);
            Property(i => i.Name).IsRequired();
            Property(i => i.Subject).IsRequired();
            Property(i => i.Description).IsRequired();
            Property(i => i.MaximumScore).IsRequired();
        }
    }
}