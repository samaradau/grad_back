using System.Data.Entity;
using System.Reflection;
using DemoLab.Data.Access.ExerciseExecutor;
using DemoLab.Data.Access.ExerciseManagement;
using DemoLab.Data.Access.Migrations;
using DemoLab.Data.Access.UserManagement;

namespace DemoLab.Data.Access.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public DbSet<CandidateTask> CandidateTasks { get; set; }

        public DbSet<CandidateTaskResult> CandidateTaskResults { get; set; }

        public DbSet<CandidateTest> CandidateTests { get; set; }

        public DbSet<CandidateTestResult> CandidateTestResults { get; set; }

        public DbSet<TestAssemblyInfo> TestAssemblies { get; set; }

        public DbSet<TestClassInfo> TestClasses { get; set; }

        public DbSet<TestMethodInfo> TestMethods { get; set; }

        public DbSet<Invite> Invites { get; set; }

        public DbSet<CandidateTaskTip> CandidateTaskTips { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
