namespace SSToolkit.Infrastructure.EntityFrameworkCore.Reference
{
    using Microsoft.EntityFrameworkCore;
    using SSToolkit.Infrastructure.EntityFrameworkCore;

    public class MyDbContext : BaseDbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MyDbContext(DbContextOptions options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Student>().HasKey(x => x.Id);
            modelBuilder.Entity<Teacher>().HasKey(x => x.Id);
            modelBuilder.Entity<Teacher>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
