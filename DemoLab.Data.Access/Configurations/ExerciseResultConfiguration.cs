using System.Data.Entity.ModelConfiguration;
using DemoLab.Data.Access.ExerciseExecutor;

namespace DemoLab.Data.Access.Configurations
{
    /// <summary>
    /// Represents an invite table configuration for Entity Framework.
    /// </summary>
    internal sealed class ExerciseResultConfiguration : EntityTypeConfiguration<CandidateExerciseResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExerciseResultConfiguration"/> class.
        /// </summary>
        public ExerciseResultConfiguration()
        {
            ToTable("CandidateExerciseResults");
            HasKey<int>(i => i.Id);
            HasRequired(i => i.CandidateExercise);
            Property(i => i.Score).HasColumnName("Score").IsRequired();
            Property(i => i.CreatorId).HasColumnName("CreatorId").IsRequired();
            Property(i => i.ModifierId).HasColumnName("ModifierId").IsRequired();
        }
    }
}
