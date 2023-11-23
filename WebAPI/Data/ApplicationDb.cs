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
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<GroupTest> GroupTests { get; set; }
    }
}
