1. Install the SSToolkit.Domain.Repositories and SSToolkit.Infrastructure.EntityFrameworkCore packages
3. Add the DB Contexts and entities
4. Register the SQL
5. Create empty database
6. Add Migration see: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
    e.g.
    dotnet ef migrations add Init --project SSToolkit.EntityFramework.Core.Reference
7. Inject the repositories

Happy coding!