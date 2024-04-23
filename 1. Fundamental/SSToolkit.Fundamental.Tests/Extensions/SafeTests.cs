namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class SafeTests
    {
        [Fact]
        public void Safe_Test()
        {
            ((IEnumerable<string>?)null)
                .Safe()
                .ShouldNotBeNull()
                .ShouldBe(Enumerable.Empty<string>());

            new List<string?> { null }.AsEnumerable()
                .Safe()
                .ShouldNotBeNull()
                .ShouldBe(Enumerable.Empty<string>());

            ((ICollection<string>?)null)
                .Safe()
                .ShouldNotBeNull()
                .ShouldBe(Enumerable.Empty<string>());

            ((ICollection<string?>)new List<string?> { null })
                .Safe()
                .ShouldNotBeNull()
                .ShouldBe(Enumerable.Empty<string>());

            ((string?)null).Safe().ShouldNotBeNull().ShouldBe(string.Empty);

            (null as IDictionary<string, string>)
                .Safe()
                .ShouldNotBeNull()
                .ShouldBe(new Dictionary<string, string>());

            (null as IDictionary<string, string>)
                .Safe()
                .ShouldNotBeNull()
                .ShouldBe(new Dictionary<string, string>());
        }
    }
}
