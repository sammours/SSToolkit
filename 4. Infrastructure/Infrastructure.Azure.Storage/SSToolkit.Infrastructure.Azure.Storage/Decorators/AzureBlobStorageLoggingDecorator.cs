namespace SSToolkit.Infrastructure.Azure.Storage.Decorators
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;
    using Microsoft.Extensions.Logging;
    using SSToolkit.Fundamental.Extensions;

    public class AzureBlobStorageLoggingDecorator : IAzureBlobStorage
    {
        private readonly IAzureBlobStorage decoretee;
        private readonly ILogger<IAzureBlobStorage> logger;

        public AzureBlobStorageLoggingDecorator(IAzureBlobStorage repository, ILogger<IAzureBlobStorage> logger)
        {
            this.decoretee = repository;
            this.logger = logger;
        }

        public async Task<bool> CopyAsync(string source, string target, string containerName = "", CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Copy '{source}' into '{target}' in container '{containerName}'.");
            return await this.decoretee.CopyAsync(source, target, containerName, cancellationToken).AnyContext();
        }

        public async Task<BlobContainerClient> GetOrCreateContainerAsync(string containerName, PublicAccessType publicAccessType = PublicAccessType.Blob, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Get or create container '{containerName}' with Public access type '{publicAccessType}'.");
            return await this.decoretee.GetOrCreateContainerAsync(containerName, publicAccessType, cancellationToken).AnyContext();
        }

        public async Task<bool> DeleteAsync(string blobName, string containerName = "", DeleteSnapshotsOption deleteSnapshotsOption = DeleteSnapshotsOption.IncludeSnapshots, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Delete '{blobName}' in container '{containerName}' with options '{deleteSnapshotsOption}'.");
            return await this.decoretee.DeleteAsync(blobName, containerName, deleteSnapshotsOption, cancellationToken).AnyContext();
        }

        public async Task<bool> DeleteContainerAsync(string containerName, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Delete container '{containerName}'.");
            return await this.decoretee.DeleteContainerAsync(containerName, cancellationToken).AnyContext();
        }

        public async Task DownloadAsync(string blobName, Stream stream, string containerName = "", CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Download blob '{blobName}' in container '{containerName}' into stream.");
            await this.decoretee.DownloadAsync(blobName, stream, containerName, cancellationToken).AnyContext();
        }

        public async Task<byte[]> DownloadAsync(string blobName, string containerName = "", CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Download blob '{blobName}' in container '{containerName}' as byte array.");
            return await this.decoretee.DownloadAsync(blobName, containerName, cancellationToken).AnyContext();
        }

        public async Task<bool> ExistsAsync(string blobName, string containerName = "", CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Check if blob '{blobName}' in container '{containerName}' does exist.");
            return await this.decoretee.ExistsAsync(blobName, containerName, cancellationToken).AnyContext();
        }

        public async Task<bool> RenameAsync(string oldName, string newName, string containerName = "", CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Rename blob '{oldName}' to '{newName}' in container '{containerName}'.");
            return await this.decoretee.RenameAsync(oldName, newName, containerName, cancellationToken).AnyContext();
        }

        public async Task<bool> UploadAsync(string blobName, byte[] bytes, string containerName = "", bool shouldReplace = false, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Upload blob '{blobName}' to container '{containerName}'. Should replace: {shouldReplace}.");
            return await this.decoretee.UploadAsync(blobName, bytes, containerName, shouldReplace, cancellationToken).AnyContext();
        }

        public async Task<bool> UploadAsync(string blobName, Stream stream, string containerName = "", bool shouldReplace = false, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Upload blob '{blobName}' to container '{containerName}'. Should replace: {shouldReplace}.");
            return await this.decoretee.UploadAsync(blobName, stream, containerName, shouldReplace, cancellationToken).AnyContext();
        }
    }
}
