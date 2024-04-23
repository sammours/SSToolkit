namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class SubstringFromTests
    {
        [Fact]
        public void SubstringFrom_Test()
        {
            (null as string).SubstringFrom("hello").ShouldBeNull();
            string.Empty.SubstringFrom("hello").ShouldBeEmpty();
            string.Empty.SubstringFrom(null).ShouldBeEmpty();
            string.Empty.SubstringFrom(string.Empty).ShouldBeEmpty();

            "aaa.bbb.aaa.bbb".SubstringFrom("bbb").ShouldBe(".aaa.bbb");
            "aaa.bbb.aaa.bbb".SubstringFromLast("aaa").ShouldBe(".bbb");
        }
    }
}
