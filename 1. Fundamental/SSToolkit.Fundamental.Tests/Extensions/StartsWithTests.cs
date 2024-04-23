namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class StartsWithTests
    {
        [Fact]
        public void StartsWith_Test()
        {
            (null as string).StartsWithAny(new List<string> { "hello" }).ShouldBeFalse();
            string.Empty.StartsWithAny(new List<string> { "hello" }).ShouldBeFalse();
            "hello world".StartsWithAny(null).ShouldBeFalse();
            "hello world".StartsWithAny(new List<string> { }).ShouldBeFalse();
            "hello world".StartsWithAny(new List<string> { "hello" }).ShouldBeTrue();
            "hello world".StartsWithAny(new List<string> { "hi", "hello" }).ShouldBeTrue();
        }
    }
}
