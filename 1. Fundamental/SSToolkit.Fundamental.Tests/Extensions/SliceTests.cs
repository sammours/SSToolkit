namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class SliceTests
    {
        [Fact]
        public void Slice_Positions()
        {
            (null as string).Slice("start", "end").ShouldBeNull();
            "cut from string to string".Slice("from", "to").ShouldBe(" string ");

            "cut from string to string".Slice(0, 3).ShouldBe("cut");
            "cut from string to string".Slice(0, 50).ShouldBe("cut from string to string");
        }
    }
}
