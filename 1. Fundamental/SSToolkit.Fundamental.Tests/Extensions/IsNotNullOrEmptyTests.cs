namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class IsNotNullOrEmptyTests
    {
        [Fact]
        public void IsNotNullOrEmpty_Test()
        {
            ((IEnumerable<string>?)null)
                .IsNotNullOrEmpty()
                .ShouldBeFalse();

            new List<string?> { }.AsEnumerable()
                .IsNotNullOrEmpty()
                .ShouldBeFalse();

            new List<string> { "not empty" }.AsEnumerable()
                .IsNotNullOrEmpty()
                .ShouldBeTrue();

            ((ICollection<string>?)null)
                .IsNotNullOrEmpty()
                .ShouldBeFalse();

            ((ICollection<string>?)new List<string?> { })
                .IsNotNullOrEmpty()
                .ShouldBeFalse();

            ((ICollection<string>?)new List<string> { "not empty" })
                .IsNotNullOrEmpty()
                .ShouldBeTrue();

            ((Stream?)null)
                .IsNotNullOrEmpty()
                .ShouldBeFalse();

            ((Stream?)new MemoryStream())
                .IsNotNullOrEmpty()
                .ShouldBeFalse();

            ((Stream?)new MemoryStream(new byte[] { 1 }))
                .IsNotNullOrEmpty()
                .ShouldBeTrue();


            ((string?)null).IsNullOrEmpty().ShouldBeTrue();
            string.Empty.IsNullOrEmpty().ShouldBeTrue();
            "not empty".IsNullOrEmpty().ShouldBeFalse();
        }
    }
}
