namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class IsValidDateTests
    {
        [Fact]
        public void IsValidDate_Test()
        {
            "Friday, 29 May 2015".IsValidDate().ShouldBeTrue();
            "Friday, 29 May 2015 05:50".IsValidDate().ShouldBeTrue();
            "Friday, 29 May 2015 05:50 AM".IsValidDate().ShouldBeTrue();
            "29.05.2015 05:50".IsValidDate().ShouldBeTrue();
            "29.05.2015 05:50 AM".IsValidDate().ShouldBeTrue();
            "2015-05-29T05:50:06".IsValidDate().ShouldBeTrue();
            "29.05.2015 05:50:06".IsValidDate().ShouldBeTrue();

            "not valid date".IsValidDate().ShouldBeFalse();
        }
    }
}
