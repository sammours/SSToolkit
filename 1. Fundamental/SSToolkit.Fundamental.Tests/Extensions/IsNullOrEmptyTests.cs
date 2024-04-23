namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class IsNullOrEmptyTests
    {
        [Fact]
        public void IsNullOrEmpty_Test()
        {
            ((IEnumerable<string>?)null)
                .IsNullOrEmpty()
                .ShouldBeTrue();

            new List<string?> { }.AsEnumerable()
                .IsNullOrEmpty()
                .ShouldBeTrue();

            new List<string> { "not empty" }.AsEnumerable()
                .IsNullOrEmpty()
                .ShouldBeFalse();

            ((ICollection<string>?)null)
                .IsNullOrEmpty()
                .ShouldBeTrue();

            ((ICollection<string>?)new List<string?> { })
                .IsNullOrEmpty()
                .ShouldBeTrue();

            ((ICollection<string>?)new List<string> { "not empty" })
                .IsNullOrEmpty()
                .ShouldBeFalse();

            ((Stream?)null)
                .IsNullOrEmpty()
                .ShouldBeTrue();

            ((Stream?)new MemoryStream())
                .IsNullOrEmpty()
                .ShouldBeTrue();

            ((Stream?)new MemoryStream(new byte[] { 1 }))
                .IsNullOrEmpty()
                .ShouldBeFalse();


            ((string?)null).IsNullOrEmpty().ShouldBeTrue();
            string.Empty.IsNullOrEmpty().ShouldBeTrue();
            "not empty".IsNullOrEmpty().ShouldBeFalse();
        }
    }
}
