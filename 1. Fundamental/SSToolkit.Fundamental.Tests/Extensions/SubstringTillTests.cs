namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class SubstringTillTests
    {
        [Fact]
        public void SubstringTill_Test()
        {
            (null as string).SubstringTill("hello").ShouldBeNull();
            string.Empty.SubstringTill("hello").ShouldBeEmpty();
            string.Empty.SubstringTill(null).ShouldBeEmpty();
            string.Empty.SubstringTill(string.Empty).ShouldBeEmpty();

            "aaa.bbb.aaa.bbb".SubstringTill("bbb").ShouldBe("aaa.");
            "aaa.bbb.aaa.bbb".SubstringTillLast("aaa").ShouldBe("aaa.bbb.");
        }
    }
}
