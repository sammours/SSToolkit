namespace SSToolkit.Infrastructure.Azure.Storage.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using global::Azure.Storage.Blobs.Models;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using SSToolkit.Fundamental.Extensions;
    using SSToolkit.Infrastructure.Azure.Storage.Decorators;
    using Xunit;

    public class AzureBlobStorageLogggingDecoratorTests
    {
        private IAzureBlobStorage azureBlobStorage;
        private ILogger<IAzureBlobStorage> logger;

        public AzureBlobStorageLogggingDecoratorTests()
        {
            this.azureBlobStorage = Substitute.For<IAzureBlobStorage>();
            this.logger = Substitute.For<ILogger<IAzureBlobStorage>>();

            this.azureBlobStorage = new AzureBlobStorageLoggingDecorator(this.azureBlobStorage, this.logger);
        }

        [Fact]
        public async Task UploadAsync_Tests()
        {
            var blobName = "blobName";
            var containerName = "container";

            var sourceBlobName = "sourceName";
            var targetBlobName = "targetName";
            await this.azureBlobStorage.UploadAsync(blobName, new byte[] { 1 }, containerName, true).AnyContext();
            await this.azureBlobStorage.UploadAsync(blobName, new byte[] { 2 }, containerName, false).AnyContext();

            await this.azureBlobStorage.DownloadAsync(blobName, containerName).AnyContext();
            await this.azureBlobStorage.DownloadAsync(blobName, new MemoryStream(), containerName).AnyContext();
            await this.azureBlobStorage.ExistsAsync(blobName, containerName).AnyContext();
            await this.azureBlobStorage.DeleteContainerAsync(containerName).AnyContext();
            await this.azureBlobStorage.DeleteAsync(blobName, containerName).AnyContext();
            await this.azureBlobStorage.GetOrCreateContainerAsync(containerName).AnyContext();
            await this.azureBlobStorage.CopyAsync(sourceBlobName, targetBlobName, containerName).AnyContext();
            await this.azureBlobStorage.RenameAsync(sourceBlobName, targetBlobName, containerName).AnyContext();

            this.logger.Received(10);
            this.logger.Received(1).LogInformation($"Upload blob '{blobName}' to container '{containerName}'. Should replace: True.");
            this.logger.Received(1).LogInformation($"Upload blob '{blobName}' to container '{containerName}'. Should replace: False.");
            this.logger.Received(1).LogInformation($"Download blob '{blobName}' in container '{containerName}' into stream.");
            this.logger.Received(1).LogInformation($"Download blob '{blobName}' in container '{containerName}' as byte array.");
            this.logger.Received(1).LogInformation($"Check if blob '{blobName}' in container '{containerName}' does exist.");
            this.logger.Received(1).LogInformation($"Delete container '{containerName}'.");
            this.logger.Received(1).LogInformation($"Delete '{blobName}' in container '{containerName}' with options '{DeleteSnapshotsOption.IncludeSnapshots}'.");
            this.logger.Received(1).LogInformation($"Get or create container '{containerName}' with Public access type '{PublicAccessType.Blob}'.");
            this.logger.Received(1).LogInformation($"Copy '{sourceBlobName}' into '{targetBlobName}' in container '{containerName}'.");
            this.logger.Received(1).LogInformation($"Rename blob '{sourceBlobName}' to '{targetBlobName}' in container '{containerName}'.");
        }
    }
}