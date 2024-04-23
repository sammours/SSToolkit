#pragma warning disable SA1200 // Using directives should be placed correctly
using SSToolkit.Domain.Repositories;
using SSToolkit.Infrastructure.EntityFrameworkCore;
using SSToolkit.Infrastructure.EntityFrameworkCore.Reference;
#pragma warning restore SA1200 // Using directives should be placed correctly

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Database
builder.Services.AddDbContext<MyDbContext>(options =>
    options.AddSqlServer(builder.Configuration.GetValue(typeof(string), "ConnectionString")?.ToString() ?? string.Empty));

// Register resiliency transaction
builder.Services.RegisterResiliencyTransaction<MyDbContext>();

builder.Services.AddLogging();

// Register Repositories
builder.Services.AddScoped<IRepository<Student>>(serviceProvider =>
    EntityFrameworkRepositoryFactory.Create<Student>(serviceProvider.GetRequiredService<MyDbContext>())
        .AddLoggingDecorator(serviceProvider.GetRequiredService<ILogger<IRepository<Student>>>()));

builder.Services.AddScoped<IRepository<Teacher>>(serviceProvider =>
    EntityFrameworkRepositoryFactory.Create<Teacher>(serviceProvider.GetRequiredService<MyDbContext>())
        .AddLoggingDecorator(serviceProvider.GetRequiredService<ILogger<IRepository<Teacher>>>()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
app.MapGet("/teachers/get", async (IRepository<Teacher> repository) =>
{
    var teacher = new Teacher
    {
        FirstName = "Teacher"
    };

    teacher = await repository.InsertAsync(teacher);

    var dbTeacher = await repository.FindOneAsync(teacher.Id);
    if (dbTeacher is not null)
    {
        await repository.DeleteAsync(dbTeacher);
    }

    return dbTeacher;
});

app.MapGet("/resiliency-transaction", async (IRepository<Teacher> repository, IResiliencyTransaction transaction) =>
{
    var teacher = new Teacher
    {
        FirstName = "Teacher"
    };

    // Should add (1 insert)
    await transaction.ExecuteAsync(async () =>
    {
        await repository.InsertAsync(teacher);
    });

    try
    {
        // Should roll back (no insert)
        await transaction.ExecuteAsync(async () =>
        {
            await repository.InsertAsync(teacher);
            throw new Exception();
        });
    }
    catch
    {
    }

    var all = await repository.FindAllAsync();

    // clear database
    await transaction.ExecuteAsync(async () =>
    {
        foreach (var item in all)
        {
            await repository.DeleteAsync(item);
        }
    });

    return all.Count() == 1;
});

app.Run();