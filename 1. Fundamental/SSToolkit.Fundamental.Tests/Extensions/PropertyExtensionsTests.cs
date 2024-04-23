namespace SSToolkit.Fundamental.Tests.Extensions
{
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class PropertyExtensionsTests
    {
        [Fact]
        public void GetPropertyName_Test()
        {
            var stub = new Stub()
            {
                FirstName = "FirstName",
                IsUnique = true
            };

            stub.GetPropertyValue("FirstName").ShouldBe("FirstName");
            stub.GetPropertyValue("IsUnique").ShouldBe("True");

            stub.GetPropertyValue<Stub, string>("FirstName").ShouldBe("FirstName");
            stub.GetPropertyValue<Stub, bool>("IsUnique").ShouldBeTrue();

            stub.GetPropertyValue(x => x.FirstName).ShouldBe("FirstName");
            stub.GetPropertyValue(x => x.IsUnique).ShouldBeTrue();

            stub.GetPropertyValue<Stub, object>(x => x.FirstName).ToString().ShouldBe("FirstName");
            stub.GetPropertyValue<Stub, object>(x => x.IsUnique).ToString().ShouldBe("True");
        }

        public class Stub
        {
            public string FirstName { get; set; } = string.Empty;

            public bool IsUnique { get; set; }
        }
    }
}
