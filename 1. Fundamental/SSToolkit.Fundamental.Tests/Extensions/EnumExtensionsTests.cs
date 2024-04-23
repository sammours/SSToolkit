namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.ComponentModel;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class EnumExtensionsTests
    {
        [Fact]
        public void ToDescription_Test()
        {
            // arrange

            // act
            var description = MockEnum.Value1.ToDescription();

            // assert
            Assert.NotNull(description);
            Assert.Equal("This is value 1", description);
        }

        [Fact]
        public void GetText_Test()
        {
            // arrange

            // act
            var description = EnumExtensions.GetText<DescriptionAttribute>(MockEnum.Value1);

            // assert
            Assert.NotNull(description);
            Assert.Equal("This is value 1", description.Description);
        }
    }

#pragma warning disable SA1201 // Elements must appear in the correct order
    public enum MockEnum
#pragma warning restore SA1201 // Elements must appear in the correct order
    {
        [Description("This is value 1")]
        Value1 = 0,
        Value2 = 1
    }
}
