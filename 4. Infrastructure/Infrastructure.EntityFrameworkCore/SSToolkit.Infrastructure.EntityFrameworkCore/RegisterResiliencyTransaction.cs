namespace SSToolkit.Infrastructure.EntityFrameworkCore
{
    using Microsoft.Extensions.DependencyInjection;

    public static partial class EntityFrameworkExtensions
    {
        /// <summary>
        /// Register resiliency transaction
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public static IServiceCollection RegisterResiliencyTransaction<T>(
            this IServiceCollection source)
            where T : BaseDbContext
        {
            source.AddScoped<IResiliencyTransaction>(serviceProvider => new ResiliencyTransaction(serviceProvider.GetRequiredService<T>()));
            return source;
        }
    }
}
