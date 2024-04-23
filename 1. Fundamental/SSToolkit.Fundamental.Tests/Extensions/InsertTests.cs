namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class InsertTests
    {
        [Fact]
        public void Insert_Test()
        {
            var list = new List<string>
            {
                "1",
                "2",
                "3",
                "5",
                "6",
            };

            list.Insert("7");
            list.Count.ShouldBe(6);
            list.PositionOf("7").ShouldBe(5);


            list.Insert("4", 3);
            list.Count.ShouldBe(7);
            list.PositionOf("4").ShouldBe(3);
        }
    }
}
