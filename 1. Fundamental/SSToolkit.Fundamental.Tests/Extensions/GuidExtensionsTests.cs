namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class GuidExtensionsTests
    {
        [Fact]
        public void ToBase64_Test()
        {
            var guid = Guid.NewGuid();
            var result = guid.ToBase64();
            result.ShouldNotBeNullOrWhiteSpace();
            result.ToNullableGuid().ShouldBe(guid);

            var resultTrim = guid.ToBase64(true);
            resultTrim.ShouldNotBeNullOrWhiteSpace();
            resultTrim.ShouldNotEndWith("=");
            resultTrim.ShouldNotEndWith("==");
            (resultTrim + "==").ToNullableGuid().ShouldBe(guid); // re-add == for it to be proper base64
        }

        [Fact]
        public void ToCode_Test()
        {
            var guid = Guid.NewGuid();
            var result = guid.ToCode();

            result.ShouldNotBeNullOrWhiteSpace();
            result.Length.ShouldBeGreaterThanOrEqualTo(15);
        }

        [Fact]
        public void ToNumber_Test()
        {
            var guid = Guid.NewGuid();
            var result = guid.ToNumber();

            result.ShouldBeGreaterThan(0);
            result.ToString().Length.ShouldBe(19);
        }

        [Fact]
        public void ToGuid_Test()
        {
            var guid = Guid.NewGuid();
            var result = guid.ToString();

            result.ToGuid().ShouldBe(guid);

            "wrong guid".ToGuid().ShouldBe(default);
        }

        [Fact]
        public void IsValidGuid_Test()
        {
            Guid.NewGuid().ToString().IsValidGuid().ShouldBeTrue();
            "wrong guid".IsValidGuid().ShouldBeFalse();

            Guid.NewGuid().As<object>().IsValidGuid().ShouldBeTrue();
            "wrong guid".As<object>().IsValidGuid().ShouldBeFalse();
        }
    }
}
