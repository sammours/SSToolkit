namespace SSToolkit.Infrastructure.Azure.Storage.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Azure;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;
    using NSubstitute;
    using Shouldly;
    using SSToolkit.Fundamental.Extensions;
    using Xunit;

    public class AzureBlobStorageTests
    {
        private AzureBlobStorage azureBlobStorage;
        private BlobContainerClient container;

        public AzureBlobStorageTests()
        {
            this.container = Substitute.For<BlobContainerClient>();
            this.azureBlobStorage = new AzureBlobStorage(this.container);
        }

        [Fact]
        public async Task UploadAsync_Tests()
        {
            var blobName = "BlobName";
            var blob = this.CreateMockBlobClient();
            this.container.GetBlobClient(blobName).Returns(blob);

            var result = await this.azureBlobStorage.UploadAsync(blobName, new byte[] { 1 }, "container", true).AnyContext();
            result.ShouldBeTrue();

            await blob.Received(1).UploadAsync(
                Arg.Is<Stream>(stream => stream.Length == 1 && ((MemoryStream)stream).ToArray()[0] == 1),
                overwrite: Arg.Is<bool>(x => x == true),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            result = await this.azureBlobStorage.UploadAsync(blobName, new byte[] { 2 }, "container", false).AnyContext();
            result.ShouldBeTrue();

            await blob.Received(1).UploadAsync(
                Arg.Is<Stream>(stream => stream.Length == 1 && ((MemoryStream)stream).ToArray()[0] == 2),
                overwrite: Arg.Is<bool>(x => x == false),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            result = await this.azureBlobStorage.UploadAsync(blobName, new MemoryStream(new byte[] { 3 }), "container", true).AnyContext();
            result.ShouldBeTrue();

            await blob.Received(1).UploadAsync(
                Arg.Is<Stream>(stream => stream.Length == 1 && ((MemoryStream)stream).ToArray()[0] == 3),
                overwrite: Arg.Is<bool>(x => x == true),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            result = await this.azureBlobStorage.UploadAsync(blobName, new MemoryStream(new byte[] { 4 }), "container", false).AnyContext();
            result.ShouldBeTrue();

            await blob.Received(1).UploadAsync(
                Arg.Is<Stream>(stream => stream.Length == 1 && ((MemoryStream)stream).ToArray()[0] == 4),
                overwrite: Arg.Is<bool>(x => x == false),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            this.container.Received(4).GetBlobClient(blobName);
        }

        [Fact]
        public async Task DownloadAsync_Tests()
        {
            var blobName = "BlobName";
            var blob = this.CreateMockBlobClient();
            this.container.GetBlobClient(blobName).Returns(blob);

            var result = await this.azureBlobStorage.DownloadAsync(blobName, "container").AnyContext();
            result.ShouldNotBeNull();
            result.Length.ShouldBe(0);

            await blob.Received(1).DownloadToAsync(destination: Arg.Any<Stream>(), cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            await this.azureBlobStorage.DownloadAsync(blobName, new MemoryStream(), "container").AnyContext();
            await blob.Received(2).DownloadToAsync(destination: Arg.Any<Stream>(), cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            this.container.Received(2).GetBlobClient(blobName);
        }

        [Fact]
        public async Task ExistsAsync_Tests()
        {
            var blobName = "BlobName";
            var blob = this.CreateMockBlobClient();
            this.container.GetBlobClient(blobName).Returns(blob);

            await this.azureBlobStorage.ExistsAsync(blobName, "container").AnyContext();

            await blob.Received(1).ExistsAsync(cancellationToken: Arg.Any<CancellationToken>()).AnyContext();
            this.container.Received(1).GetBlobClient(blobName);
        }

        [Fact]
        public async Task DeleteAsync_Tests()
        {
            var blobName = "BlobName";
            var blob = this.CreateMockBlobClient();
            this.container.GetBlobClient(blobName).Returns(blob);

            await this.azureBlobStorage.DeleteAsync(blobName, "container", DeleteSnapshotsOption.IncludeSnapshots).AnyContext();

            await blob.Received(1).DeleteAsync(
                Arg.Is<DeleteSnapshotsOption>(x => x == DeleteSnapshotsOption.IncludeSnapshots),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            await this.azureBlobStorage.DeleteAsync(blobName, "container", DeleteSnapshotsOption.None).AnyContext();

            await blob.Received(1).DeleteAsync(
                Arg.Is<DeleteSnapshotsOption>(x => x == DeleteSnapshotsOption.None),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            await this.azureBlobStorage.DeleteAsync(blobName, "container", DeleteSnapshotsOption.OnlySnapshots).AnyContext();

            await blob.Received(1).DeleteAsync(
                Arg.Is<DeleteSnapshotsOption>(x => x == DeleteSnapshotsOption.OnlySnapshots),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            await this.azureBlobStorage.DeleteAsync(blobName, "container").AnyContext();

            await blob.Received(2).DeleteAsync(
                Arg.Is<DeleteSnapshotsOption>(x => x == DeleteSnapshotsOption.IncludeSnapshots),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            this.container.Received(4).GetBlobClient(blobName);
        }

        [Fact]
        public async Task CopyAsync_Tests()
        {
            var sourceBlobName = "SourceBlobName";
            var targetBlobName = "TargetBlobName";

            var uri = new Uri($"https://someurl.com/{sourceBlobName}");
            // for source a real instance is needed for the uri
            var sourceBlob = Substitute.ForPartsOf<BlobClient>(uri, new BlobClientOptions());
            sourceBlob.ExistsAsync(Arg.Any<CancellationToken>()).Returns(x => this.CreateResponseMock(true));

            var targetBlob = this.CreateMockBlobClient();
            this.container.GetBlobClient(sourceBlobName).Returns(sourceBlob);
            this.container.GetBlobClient(targetBlobName).Returns(targetBlob);

            await this.azureBlobStorage.CopyAsync(sourceBlobName, targetBlobName, "container").AnyContext();

            await targetBlob.Received(1)
                 .StartCopyFromUriAsync(source: Arg.Is<Uri>(x => x == sourceBlob.Uri), cancellationToken: Arg.Any<CancellationToken>())
                 .AnyContext();
        }


        [Fact]
        public async Task RenameAsync_Tests()
        {
            var sourceBlobName = "SourceBlobName";
            var targetBlobName = "TargetBlobName";

            var uri = new Uri($"https://someurl.com/{sourceBlobName}");
            var sourceBlobInstance = Substitute.ForPartsOf<BlobClient>(uri, new BlobClientOptions());
            var sourceBlobMock = this.CreateMockBlobClient();

            // for source a real instance is needed for the uri
            var targetBlob = this.CreateMockBlobClient();
            this.container.GetBlobClient(sourceBlobName).Returns(
                x => sourceBlobInstance,
                always => sourceBlobMock);
            this.container.GetBlobClient(targetBlobName).Returns(targetBlob);

            sourceBlobInstance.ExistsAsync(Arg.Any<CancellationToken>()).Returns(x => this.CreateResponseMock(true));

            await this.azureBlobStorage.RenameAsync(sourceBlobName, targetBlobName, "container").AnyContext();

            await targetBlob.Received(1)
                 .StartCopyFromUriAsync(source: Arg.Is<Uri>(x => x == sourceBlobInstance.Uri), cancellationToken: Arg.Any<CancellationToken>())
                 .AnyContext();

            await sourceBlobMock.Received(1).DeleteAsync(
                Arg.Is<DeleteSnapshotsOption>(x => x == DeleteSnapshotsOption.IncludeSnapshots),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();

            this.container.Received(2).GetBlobClient(sourceBlobName);
            this.container.Received(1).GetBlobClient(targetBlobName);
        }

        [Fact]
        public async Task GetOrCreateContainerAsync_Test()
        {
            var container = await this.azureBlobStorage.GetOrCreateContainerAsync("container").AnyContext();
            container.ShouldNotBeNull().ShouldBe(this.container);

            await container.Received(1).CreateIfNotExistsAsync(Arg.Any<PublicAccessType>(),
                cancellationToken: Arg.Any<CancellationToken>()).AnyContext();
        }

        [Fact]
        public async Task DeleteContainerAsync_Test()
        {
            this.container.ExistsAsync(Arg.Any<CancellationToken>())
                .Returns(x => this.CreateResponseMock(false), always => this.CreateResponseMock(true));

            var result = await this.azureBlobStorage.DeleteContainerAsync("container").AnyContext();
            result.ShouldBeFalse();

            result = await this.azureBlobStorage.DeleteContainerAsync("container").AnyContext();
            result.ShouldBeTrue();
        }

        private BlobClient CreateMockBlobClient()
        {
            var blob = Substitute.For<BlobClient>();
            return blob;
        }

        private Response<bool> CreateResponseMock(bool val)
        {
            var response = Substitute.For<Response<bool>>();
            response.Value.Returns(val);
            return response;
        }
    }
}