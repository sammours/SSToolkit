namespace SSToolkit.Infrastructure.Azure.Storage
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using SSToolkit.Infrastructure.Azure.Storage.Decorators;

    public static partial class AzureBlobStorageExtensions
    {
        /// <summary>
        /// Register azure blob storage
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="connectionString">The storage connection string</param>
        /// <param name="containerName">The main container (default: main) (Container could be specified later per request) See for valid container name: <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="shouldIncludeLogging">Should include logging decorator (default: true)</param>
        /// <returns>Return the service collection</returns>
        public static IServiceCollection RegisterAzureBlobStorage(this IServiceCollection services, string connectionString, string containerName = "main", bool shouldIncludeLogging = true)
        {
            services.AddScoped<IAzureBlobStorage>(serviceProvider =>
            {
                var repository = new AzureBlobStorage(new AzureBlobStorageOptions()
                {
                    ContainerName = containerName,
                    ConnectionString = connectionString
                });

                if (shouldIncludeLogging)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<IAzureBlobStorage>>();
                    return repository.AddLoggingDecorator(logger);
                }

                return repository;
            });

            return services;
        }

        /// <summary>
        /// Register azure blob storage
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="options">Blob storage options</param>
        /// <param name="shouldIncludeLogging">Should include logging decorator (default: true)</param>
        /// <returns>Return the service collection</returns>
        public static IServiceCollection RegisterAzureBlobStorage(this IServiceCollection services, AzureBlobStorageOptions options, bool shouldIncludeLogging = true)
        {
            if (options == null)
            {
                return services;
            }

            return services.RegisterAzureBlobStorage(options.ConnectionString, options.ContainerName, shouldIncludeLogging);
        }

        /// <summary>
        /// Add logging decorator
        /// </summary>
        /// <param name="source">The decoretee</param>
        /// <param name="logger">Logger</param>
        /// <returns>Decorated source</returns>
        public static IAzureBlobStorage AddLoggingDecorator(this AzureBlobStorage source, ILogger<IAzureBlobStorage> logger)
        {
            return new AzureBlobStorageLoggingDecorator(source, logger);
        }
    }
}
