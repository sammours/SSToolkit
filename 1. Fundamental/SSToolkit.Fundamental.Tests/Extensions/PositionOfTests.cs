namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class PositionOfTests
    {
        [Fact]
        public void IsBase64_Test()
        {
            var list = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5",
            };

            list.PositionOf("3").ShouldBe(2);
            list.PositionOf(x => x == "4").ShouldBe(3);
        }
    }
}
