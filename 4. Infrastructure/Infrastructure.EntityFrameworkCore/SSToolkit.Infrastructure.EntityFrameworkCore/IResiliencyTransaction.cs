namespace SSToolkit.Infrastructure.EntityFrameworkCore
{
    public interface IResiliencyTransaction
    {
        /// <summary>
        /// Execute operation async
        /// <br></br>
        /// <br>More details: <see href="https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency"/></br>
        /// </summary>
        /// <param name="operation">The operation</param>
        /// <returns></returns>
        Task ExecuteAsync(Func<Task> operation);

        /// <summary>
        /// Execute operation async with returned value
        /// <br></br>
        /// <br>More details: <see href="https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency"/></br>
        /// </summary>
        /// <param name="operation">The operation</param>
        /// <returns></returns>
        Task<T> ExecuteResultAsync<T>(Func<Task<T>> operation)
            where T : class;
    }
}
