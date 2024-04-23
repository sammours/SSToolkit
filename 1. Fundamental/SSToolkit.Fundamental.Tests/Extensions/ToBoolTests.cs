namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class ToBoolTests
    {
        [Fact]
        public void ToBool_Test()
        {
            (null as string).ToBool().ShouldBeFalse();
            (null as string).ToBool(@default: true).ShouldBeTrue();

            (null as string).ToNullableBool(@default: null).ShouldBeNull();

            string.Empty.ToBool().ShouldBeFalse();
            string.Empty.ToBool(@default: true).ShouldBeTrue();

            "true".ToBool().ShouldBeTrue();
            "true".ToBool(@default: true).ShouldBeTrue();
            "true".ToBool(@default: false).ShouldBeTrue();

            "false".ToBool().ShouldBeFalse();
            "false".ToBool(@default: true).ShouldBeFalse();
            "false".ToBool(@default: false).ShouldBeFalse();
        }
    }
}
