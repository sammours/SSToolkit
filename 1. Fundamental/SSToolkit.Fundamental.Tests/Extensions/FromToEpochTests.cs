namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class FromToEpochTests
    {
        [Fact]
        public void ForEach_Test()
        {
            var date = new DateTime(2000, 12, 12, 12, 12, 12);
            DateTime? date2 = new DateTime(2000, 12, 12, 12, 12, 12);

            date.ToEpoch().FromEpoch().ShouldBe(date);
            date2.ToEpoch().FromEpoch().ShouldBe(date2);
        }
    }
}
