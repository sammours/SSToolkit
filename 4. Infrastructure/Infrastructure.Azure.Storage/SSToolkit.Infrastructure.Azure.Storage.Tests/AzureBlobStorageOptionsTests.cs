namespace SSToolkit.Infrastructure.Azure.Storage.Tests
{
    using System;
    using System.Threading.Tasks;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class AzureBlobStorageOptionsTests
    {
        [Fact]
        public void GetContainerName_Tests()
        {
            var options = new AzureBlobStorageOptions();
            options.GetContainerName(string.Empty).ShouldBe("main");
            options.GetContainerName(string.Empty).ShouldBe("main");
            options.GetContainerName("different").ShouldBe("different");

            options.ContainerName = "secondary";
            options.GetContainerName(string.Empty).ShouldBe("secondary");
            options.GetContainerName(string.Empty).ShouldBe("secondary");
            options.GetContainerName("different").ShouldBe("different");

            options.ContainerName = string.Empty;
            this.ExceptionAssertion<ArgumentNullException>(() => options.GetContainerName(string.Empty));

            options.ContainerName = string.Empty;
            this.ExceptionAssertion<ArgumentNullException>(() => options.GetContainerName(string.Empty));

            // Validate Container name
            this.ExceptionAssertion<ArgumentException>(() => options.GetContainerName("Sample-Container")); // contains capital
            this.ExceptionAssertion<ArgumentException>(() => options.GetContainerName("sample--container")); // consecutive dashes not allowed
            this.ExceptionAssertion<ArgumentException>(() => options.GetContainerName("sample-")); // ends with -
            this.ExceptionAssertion<ArgumentException>(() => options.GetContainerName("sa")); // less than 3 letters
            this.ExceptionAssertion<ArgumentException>(() => options.GetContainerName("samplecontainer123samplecontainer123samplecontainer123samplecontainer123samplecontainer123samplecontainer123")); // more than 63 letters
        }

        private void ExceptionAssertion<T>(Action delg, string? errorMessage = null)
            where T : Exception
        {
            var ex = Assert.Throws<T>(delg);
            Assert.NotNull(ex);
            Assert.NotNull(ex.Message);
            if (errorMessage.IsNotNullOrEmpty())
            {
                Assert.StartsWith(errorMessage, ex.Message);
            }
        }
    }
}