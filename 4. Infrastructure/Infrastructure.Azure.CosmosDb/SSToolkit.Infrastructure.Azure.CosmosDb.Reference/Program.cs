#pragma warning disable SA1200 // Using directives should be placed correctly
using Microsoft.Azure.Cosmos;
using SSToolkit.Infrastructure.Azure.CosmosDb;
using SSToolkit.Infrastructure.Azure.CosmosDb.Reference;
#pragma warning restore SA1200 // Using directives should be placed correctly

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

var connectionString = builder.Configuration.GetValue(typeof(string), "ConnectionString").ToString() ?? string.Empty;

// https://docs.microsoft.com/en-us/azure/cosmos-db/index-policy
var customerIndexingPolicy = CosmosDbIndexingPolicyFactory.Create(automatic: true, indexingMode: Microsoft.Azure.Cosmos.IndexingMode.Consistent)
                    .AddIncludedPath<Customer>("/*") // Index should always contain /* either in Include or exclude
                    .AddExcludedPath<Customer>(x => x.Discriminator)
                    .AddCompositePath<Customer>((x => x.Name, CompositePathSortOrder.Ascending), (x => x.Location, CompositePathSortOrder.Descending))
                    .AddSpatialPath<Customer>("/Name/*");

builder.Services.AddScoped<ICosmosDbRepository<Customer>>(serviceProvider =>
        CosmosDbRepositoryFactory.Create<Customer>(connectionString: connectionString, partitionKey: x => x.Location, database: "Brain",
        indexingPolicy: customerIndexingPolicy)
        .AddLoggingDecorator(serviceProvider.GetRequiredService<ILogger<ICosmosDbRepository<Customer>>>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/customers", async (ICosmosDbRepository<Customer> repository) =>
{
    var customer = new Customer
    {
        Name = "Customer",
        Location = "Germany"
    };

    customer = await repository.InsertAsync(customer);
    var dbCustomer = await repository.FindOneAsync(customer.Id.ToString());
    await repository.DeleteAsync(customer.Id);
    return dbCustomer;
});

app.Run();