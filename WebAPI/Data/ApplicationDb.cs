using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>()
                .HasOne(t => t.GroupTest)
                .WithMany(g => g.Tests)
                .HasForeignKey(t => t.GroupTestId);

            modelBuilder.Entity<TestResult>()
                .HasOne(tr => tr.Test)
                .WithMany()
                .HasForeignKey(tr => tr.TestId);

            modelBuilder.Entity<GroupTest>()
                .HasOne(t => t.TestPlan)
                .WithMany(g => g.GroupTests)
                .HasForeignKey(t => t.TestPlanId);
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<GroupTest> GroupTests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TestPlan> TestPlans { get; set; }
    }
}
