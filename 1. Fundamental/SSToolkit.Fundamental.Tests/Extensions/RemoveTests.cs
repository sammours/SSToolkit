namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class RemoveTests
    {
        [Fact]
        public void Remove_Test()
        {
            "To remove this".Remove("remove").ShouldBe("To  this");
        }
    }
}
