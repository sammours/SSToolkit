namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class ToDecimalTests
    {
        [Fact]
        public void ToDecimal_Test()
        {
            (null as string).ToDecimal().ShouldBe(0);
            (null as string).ToDecimal(@default: 0).ShouldBe(0);

            (null as string).ToNullableDecimal(@default: null).ShouldBeNull();

            string.Empty.ToDecimal().ShouldBe(0);
            string.Empty.ToDecimal(@default: 0).ShouldBe(0);

            "100".ToDecimal().ShouldBe(100);
            "100.0".ToDecimal().ShouldBe(100);
            "11,111.00".ToDecimal().ShouldBe(11111.00m);

            "wrong".ToDecimal().ShouldBe(0);
        }
    }
}
