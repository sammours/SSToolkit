#pragma warning disable SA1200 // Using directives should be placed correctly
using MediatR;
using SSToolkit.Application.Commands.Core;
using SSToolkit.Application.Commands.Core.Reference;
using SSToolkit.Application.Commands.Core.Reference.Commands;
using SSToolkit.Application.Commands.Core.Reference.CustomBehaviors;
using SSToolkit.Application.Commands.Core.Reference.Queries;
#pragma warning restore SA1200 // Using directives should be placed correctly

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

// Injecting CQRS
builder.Services.AddMediatRExtensions(typeof(GetUserByIdQuery).Assembly);
builder.Services.RegisterMediatRPipeLine();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryOnlyBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandOnlyBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ConditionalBehavior<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/query", async (IMediator mediator) =>
{
    var query = await mediator.Send(new GetUserByIdQuery(1), CancellationToken.None).ConfigureAwait(false);
    return query.Result?.User;
})
.WithName("query");

app.MapGet("/command", async (IMediator mediator) =>
{
    var command = await mediator.Send(new SaveUserCommand(new User()), CancellationToken.None).ConfigureAwait(false);
    return command.Status.ToString();
})
.WithName("command");

app.MapGet("/conditional", async (IMediator mediator) =>
{
    return (await mediator.Send(new ConditionalCommand(), CancellationToken.None).ConfigureAwait(false))
                .Status.ToString();
})
.WithName("conditional");

app.Run();
