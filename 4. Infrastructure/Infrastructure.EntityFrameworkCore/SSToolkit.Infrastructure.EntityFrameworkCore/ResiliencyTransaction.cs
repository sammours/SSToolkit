namespace SSToolkit.Infrastructure.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;
    using SSToolkit.Fundamental.Extensions;

    public class ResiliencyTransaction : IResiliencyTransaction
    {
        private readonly BaseDbContext context;

        public ResiliencyTransaction(BaseDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Execute operation async
        /// <br></br>
        /// <br>More details: <see href="https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency"/></br>
        /// </summary>
        /// <param name="operation">The operation</param>
        /// <returns></returns>
        public async Task ExecuteAsync(Func<Task> operation)
        {
            if (this.context.Database.CurrentTransaction == null)
            {
                var strategy = this.context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = this.context.Database.BeginTransaction())
                    {
                        try
                        {
                            await operation().AnyContext();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync().AnyContext();
                            throw;
                        }
                    }
                }).AnyContext();
            }
            else
            {
                await operation().AnyContext();
            }
        }

        /// <summary>
        /// Execute operation async with returned value
        /// <br></br>
        /// <br>More details: <see href="https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency"/></br>
        /// </summary>
        /// <param name="operation">The operation</param>
        /// <returns></returns>
        public async Task<T> ExecuteResultAsync<T>(Func<Task<T>> operation)
            where T : class
        {
            if (this.context.Database.CurrentTransaction == null)
            {
                var strategy = this.context.Database.CreateExecutionStrategy();
                return await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = this.context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = await operation().AnyContext();
                            transaction.Commit();
                            return result;
                        }
                        catch (Exception)
                        {
                            await transaction.RollbackAsync().AnyContext();
                            throw;
                        }
                    }
                }).AnyContext();
            }
            else
            {
                return await operation().AnyContext();
            }
        }
    }
}
