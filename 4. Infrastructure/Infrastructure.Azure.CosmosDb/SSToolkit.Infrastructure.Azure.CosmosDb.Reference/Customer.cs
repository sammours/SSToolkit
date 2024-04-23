namespace SSToolkit.Infrastructure.Azure.CosmosDb.Reference
{
    using SSToolkit.Infrastructure.Azure.CosmosDb;

    public class Customer : CosmosDbEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
    }
}
