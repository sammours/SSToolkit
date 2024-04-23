namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public partial class IsBase64Tests
    {
        [Fact]
        public void IsBase64_Test()
        {
            var base64String = "UHl0aG9uIGlzIGZ1bg==";
            base64String.IsBase64().ShouldBeTrue();

            var botBase64String = "UHl0aG9uIGlzIGZ1bg";
            botBase64String.IsBase64().ShouldBeFalse();
        }
    }
}
