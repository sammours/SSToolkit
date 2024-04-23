namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class IsValidRegexTests
    {
        [Fact]
        public void IsValidRegex_Tests()
        {
            "aaa".IsValidRegex("[a-z]{1,}").ShouldBeTrue();
            "123".IsValidRegex("[a-z]{1,}").ShouldBeFalse();
        }

        [Fact]
        public void IsValidRegexPattern_Tests()
        {
            "[a-z]{1,}".IsValidRegexPattern().ShouldBeTrue();
            "[a-z]{1,}!)(#!".IsValidRegexPattern().ShouldBeFalse();
        }
    }
}