namespace SSToolkit.Fundamental.Tests.Extensions
{
    using System.Collections.Generic;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class ValueOrDefaultTests
    {
        [Fact]
        public void ValueOfDefault_Tests()
        {
            (null as Dictionary<string, string>).ValueOrDefault("key").ShouldBeNull();
            new Dictionary<string, string>().ValueOrDefault("key").ShouldBeNull();
            new Dictionary<string, string>().ValueOrDefault("key", "default").ShouldBe("default");
            new Dictionary<string, string>().ValueOrDefault("key", "default").ShouldBe("default");

            var dic = new Dictionary<string, string>()
            {
                {"key1", "val1" },
                {"key2", "val2" }
            };

            dic.ValueOrDefault("key1").ShouldNotBeNull();
            dic.ValueOrDefault("key1").ShouldBe("val1");
            dic.ValueOrDefault("key3").ShouldBeNull();
            dic.ValueOrDefault("key3", "default value").ShouldBe("default value");
        }
    }
}
