namespace SSToolkit.Infrastructure.EntityFrameworkCore
{
    public class EntityFrameworkConfiguration
    {
        public EntityFrameworkConfiguration(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public bool EnableSensitiveDataLogging { get; set; } = true;

        public bool EnableDetailedErrors { get; set; }
    }
}
