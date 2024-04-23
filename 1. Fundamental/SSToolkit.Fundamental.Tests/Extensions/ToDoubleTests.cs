namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class ToDoubleTests
    {
        [Fact]
        public void ToDouble_Test()
        {
            (null as string).ToDouble().ShouldBe(0);
            (null as string).ToDouble(@default: 0).ShouldBe(0);

            (null as string).ToNullableDouble(@default: null).ShouldBeNull();

            string.Empty.ToDouble().ShouldBe(0);
            string.Empty.ToDouble(@default: 0).ShouldBe(0);

            "100".ToDouble().ShouldBe(100);
            "100.00".ToDouble().ShouldBe(100);
            "11,100.00".ToDouble().ShouldBe(11100d);
            "wrong".ToDouble().ShouldBe(0);
        }
    }
}
