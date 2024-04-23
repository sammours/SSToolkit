namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class IsTests
    {
        [Fact]
        public void IsClass_Test()
        {
            var sut = (object)new Stub();

            sut.Is<Stub>().ShouldBeTrue();
            sut.Is<IsTests>().ShouldBeFalse();
        }

        [Fact]
        public void IsString_Test()
        {
            var sut = (object)"Test";

            sut.Is<string>().ShouldBeTrue();
            sut.Is<int>().ShouldBeFalse();
        }

#pragma warning disable SA1201 // Elements must appear in the correct order
        public class Stub
#pragma warning restore SA1201 // Elements must appear in the correct order
        {
        }
    }
}
