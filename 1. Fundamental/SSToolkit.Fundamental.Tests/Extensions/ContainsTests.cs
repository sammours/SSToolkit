namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class ContainsTests
    {
        [Fact]
        public void Contains_Test()
        {
            // arrange
            var str = "item1";
            var list = new List<string>()
            {
                "item1",
                "item2",
                "item3"
            };

            // act
            var strContains = str.Contains("item1", System.StringComparison.OrdinalIgnoreCase);
            var strContainsAny = str.ContainsAny(list.ToArray());
            var listContains = list.Contains("item3");

            // assert
            Assert.True(strContains);
            Assert.True(strContainsAny);
            Assert.True(listContains);
        }
    }
}
