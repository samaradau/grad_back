using System.Data.Entity.ModelConfiguration;
using DemoLab.Data.Access.ExerciseManagement;

namespace DemoLab.Data.Access.Configurations
{
    /// <summary>
    /// Represents an invite table configuration for Entity Framework.
    /// </summary>
    internal sealed class CandidateTaskTipConfiguration : EntityTypeConfiguration<CandidateTaskTip>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateTaskTipConfiguration"/> class.
        /// </summary>
        public CandidateTaskTipConfiguration()
        {
            ToTable("CandidateTaskTips");
            HasKey<int>(tip => tip.Id);
            Property(tip => tip.Text).IsRequired();
            HasRequired(tip => tip.CandidateTask).WithMany(task => task.Tips);
        }
    }
}
