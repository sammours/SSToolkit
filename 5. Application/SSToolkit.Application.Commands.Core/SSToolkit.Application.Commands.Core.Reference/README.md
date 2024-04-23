- Add SSToolkit.Application.Commands.Core
- Register services 
var service = [any class or interface where the commands/queries exist];
builder.Services.AddMediatRExtensions(typeof(service).Assembly);

- Create the commands and queries as desired