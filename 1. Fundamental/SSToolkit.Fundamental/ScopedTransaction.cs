namespace SSToolkit.Fundamental
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;
    using SSToolkit.Fundamental.Extensions;

    public class ScopedTransaction
    {
        /// <summary>
        /// Execute scoped operation async
        /// <br></br>
        /// <br></br>
        /// More information: <see href="https://docs.microsoft.com/en-us/dotnet/framework/data/transactions/implementing-an-implicit-transaction-using-transaction-scope"/>
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(Func<Task> operation)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                await operation().AnyContext();
                scope.Complete();
            }
        }

        /// <summary>
        /// Execute scoped operation async with returned value
        /// <br></br>
        /// <br></br>
        /// More information: <see href="https://docs.microsoft.com/en-us/dotnet/framework/data/transactions/implementing-an-implicit-transaction-using-transaction-scope"/>
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>The result of the operation</returns>
        public async Task<T> ExecuteResultAsync<T>(Func<Task<T>> operation)
            where T : class
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var result = await operation().AnyContext();
                scope.Complete();
                return result;
            }
        }
    }
}
