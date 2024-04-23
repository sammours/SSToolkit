namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class SafeEqualsTests
    {
        [Fact]
        public void SafeEquals_Test()
        {
            (null as string).SafeEquals("val").ShouldBeFalse();
            string.Empty.SafeEquals("val").ShouldBeFalse();
            "val".SafeEquals("val").ShouldBeTrue();
            "val".SafeEquals("VaL").ShouldBeTrue();
            "val1".SafeEquals("VaL").ShouldBeFalse();
        }
    }
}
