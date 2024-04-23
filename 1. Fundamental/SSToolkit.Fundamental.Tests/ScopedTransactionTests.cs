namespace SSToolkit.Fundamental
{
    using System.Threading.Tasks;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class ScopedTransactionTests
    {
        [Fact]
        public async Task ToDescription_Test()
        {
            var transaction = new ScopedTransaction();

            // Async
            await transaction.ExecuteAsync(() =>
            {
                Assert.True(true);
                return Task.CompletedTask;
            }).AnyContext();

            // Sync
            var transaction2 = new ScopedTransaction();
            transaction2.ExecuteAsync(() =>
            {
                Assert.True(true);
                return Task.CompletedTask;
            }).Wait();

            // With result
            var transaction3 = new ScopedTransaction();
            var result = await transaction.ExecuteResultAsync(async () =>
            {
                return await Task.FromResult("Result").AnyContext();
            }).AnyContext();

            result.ShouldNotBeNull();
            result.ShouldBe("Result");
        }
    }
}
