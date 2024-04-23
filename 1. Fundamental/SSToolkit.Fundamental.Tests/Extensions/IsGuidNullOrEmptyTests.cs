namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class IsGuidNullOrEmptyTests
    {
        [Fact]
        public void IsGuidNullOrEmpty_Test()
        {
            ((Guid?)null).IsGuidNullOrEmpty().ShouldBeTrue();
            Guid.Empty.IsGuidNullOrEmpty().ShouldBeTrue();
            Guid.NewGuid().IsGuidNullOrEmpty().ShouldBeFalse();
        }
    }
}
