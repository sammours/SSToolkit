namespace SSToolkit.Infrastructure.EntityFrameworkCore
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;

    public partial class BaseDbContext : DbContext
    {
        public BaseDbContext()
        {
        }

        public BaseDbContext([NotNull] DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
