namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class ToStringTests
    {
        [Fact]
        public void ToString_Test()
        {
            (null as IEnumerable<string>).ToString(";").ShouldBeEmpty();
            Enumerable.Empty<string>().ToString(";").ShouldBeEmpty();

            new List<string>() { "first", "second" }.ToString(";").ShouldBe("first;second");
            new List<string>() { "first", "second" }.ToString(" - ").ShouldBe("first - second");
        }
    }
}
