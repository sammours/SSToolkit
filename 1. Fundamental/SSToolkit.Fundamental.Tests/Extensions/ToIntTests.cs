namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class ToIntTests
    {
        [Fact]
        public void ToInt_Test()
        {
            (null as string).ToInt().ShouldBe(0);
            (null as string).ToInt(@default: 0).ShouldBe(0);

            (null as string).ToNullableInt(@default: null).ShouldBeNull();

            string.Empty.ToInt().ShouldBe(0);
            string.Empty.ToInt(@default: 0).ShouldBe(0);

            "100".ToInt().ShouldBe(100);
            "wrong".ToDecimal().ShouldBe(0);
        }
    }
}
