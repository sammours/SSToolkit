namespace SSToolkit.Infrastructure.Azure.CosmosDb.Tests
{
    public class Stub : CosmosDbEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Location { get; set; } = string.Empty;
    }
}
