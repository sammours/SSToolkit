namespace SSToolkit.Fundamental.Tests.Extensions
{
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class DumpTests
    {
        [Fact]
        public void Dump_Test()
        {
            // arrange
            var num = 1;

            // act
            var json = num.Dump();

            // assert
            Assert.NotNull(json);
            Assert.True(json.Is<string>());
            Assert.Equal("1", json);
        }
    }
}
