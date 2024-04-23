namespace SSToolkit.Infrastructure.Azure.Storage
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;
    using SSToolkit.Fundamental.Extensions;

    public class AzureBlobStorage : IAzureBlobStorage
    {
        public AzureBlobStorage(AzureBlobStorageOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.ConnectionString.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(options.ConnectionString));
            }

            if (options.ContainerName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(options.ContainerName));
            }

            this.Options = options;
            this.Container = new BlobContainerClient(options.ContainerName, options.ContainerName);
        }

        public AzureBlobStorage(BlobContainerClient container)
        {
            this.Container = container ?? throw new ArgumentNullException(nameof(container));

            this.Options = new AzureBlobStorageOptions();
        }

        protected AzureBlobStorageOptions Options { get; }

        protected BlobContainerClient Container { get; }

        /// <summary>
        /// Copy a source blob into the target (The source will not be deleted)
        /// </summary>
        /// <param name="source">The source blob name</param>
        /// <param name="target">The target blob name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options). See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when copied</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="FileNotFoundException">FileNotFoundException</exception>
        public async Task<bool> CopyAsync(string source, string target, string containerName = "", CancellationToken cancellationToken = default)
        {
            if (source.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(target));
            }


            var container = await this.GetOrCreateContainerAsync(containerName, cancellationToken: cancellationToken).AnyContext();
            var sourceBlobClient = container.GetBlobClient(source);
            var exists = await sourceBlobClient.ExistsAsync(cancellationToken).AnyContext();
            if (!exists)
            {
                throw new FileNotFoundException($"Source blob {source} could not be found");
            }

            var targetBlobClient = container.GetBlobClient(target);
            await targetBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri, cancellationToken: cancellationToken).AnyContext();
            return true;
        }

        /// <summary>
        /// Delete a blob
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options). See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="deleteSnapshotsOption">(Default: Full deletion (IncludeSnapshots))</param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when deleted</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public async Task<bool> DeleteAsync(string blobName, string containerName = "", DeleteSnapshotsOption deleteSnapshotsOption = DeleteSnapshotsOption.IncludeSnapshots, CancellationToken cancellationToken = default)
        {
            if (blobName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            var container = await this.GetOrCreateContainerAsync(containerName, cancellationToken: cancellationToken).AnyContext();
            var blobClient = container.GetBlobClient(blobName);
            await blobClient.DeleteAsync(deleteSnapshotsOption, cancellationToken: cancellationToken).AnyContext();
            return true;
        }

        /// <summary>
        /// Download to stream
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="stream">The stream to download the blob to</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options). See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public async Task DownloadAsync(string blobName, Stream stream, string containerName = "", CancellationToken cancellationToken = default)
        {
            if (blobName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var container = await this.GetOrCreateContainerAsync(containerName, cancellationToken: cancellationToken).AnyContext();
            var blobClient = container.GetBlobClient(blobName);
            await blobClient.DownloadToAsync(stream, cancellationToken).AnyContext();
        }

        /// <summary>
        /// Download to bytes
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options). See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The byte array</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public async Task<byte[]> DownloadAsync(string blobName, string containerName = "", CancellationToken cancellationToken = default)
        {
            if (blobName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            using (var memoryStream = new MemoryStream())
            {
                await this.DownloadAsync(blobName, memoryStream, containerName, cancellationToken).AnyContext();
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Check if blob does exist
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options). See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when exists; otherwise, <c>false</c></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public async Task<bool> ExistsAsync(string blobName, string containerName = "", CancellationToken cancellationToken = default)
        {
            if (blobName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            var container = await this.GetOrCreateContainerAsync(containerName, cancellationToken: cancellationToken).AnyContext();
            var blobClient = container.GetBlobClient(blobName);
            return await blobClient.ExistsAsync(cancellationToken).AnyContext();
        }

        /// <summary>
        /// Rename a blob
        /// </summary>
        /// <param name="oldName">Old name</param>
        /// <param name="newName">New name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options). See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when renamed; otherwise, <c>false</c></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public async Task<bool> RenameAsync(string oldName, string newName, string containerName = "", CancellationToken cancellationToken = default)
        {
            if (oldName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(oldName));
            }

            if (newName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(newName));
            }

            var copySucceeded = await this.CopyAsync(oldName, newName, containerName, cancellationToken).AnyContext();
            if (copySucceeded)
            {
                await this.DeleteAsync(oldName, containerName, DeleteSnapshotsOption.IncludeSnapshots, cancellationToken).AnyContext();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Upload a blob byte array
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="bytes">Byte array</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options). See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="shouldReplace"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when uploaded</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public async Task<bool> UploadAsync(string blobName, byte[] bytes, string containerName = "", bool shouldReplace = false, CancellationToken cancellationToken = default)
        {
            if (blobName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            if (bytes.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            return await this.UploadAsync(blobName, new MemoryStream(bytes), containerName, shouldReplace, cancellationToken).AnyContext();
        }

        /// <summary>
        /// Upload a blob stream
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="stream">The stream</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options). See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="shouldReplace"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when uploaded</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public async Task<bool> UploadAsync(string blobName, Stream stream, string containerName = "", bool shouldReplace = false, CancellationToken cancellationToken = default)
        {
            if (blobName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(blobName));
            }

            if (stream.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var container = await this.GetOrCreateContainerAsync(containerName, cancellationToken: cancellationToken).AnyContext();
            var blobClient = container.GetBlobClient(blobName);
            await blobClient.UploadAsync(stream, overwrite: shouldReplace, cancellationToken: cancellationToken).AnyContext();
            return true;
        }

        /// <summary>
        /// Delete a container when found
        /// </summary>
        /// <param name="containerName">The container name. See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when deleted; otherwise, <c>false</c></returns>
        public async Task<bool> DeleteContainerAsync(string containerName, CancellationToken cancellationToken = default)
        {
            var container = this.Container;
            if (container == null)
            {
                container = new BlobContainerClient(this.Options.ConnectionString, this.Options.GetContainerName(containerName));
            }

            if (await container.ExistsAsync(cancellationToken).AnyContext())
            {
                await container.DeleteAsync(cancellationToken: cancellationToken).AnyContext();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Create or Get container by name
        /// </summary>
        /// <param name="containerName">The container name. See for valid container name <see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></param>
        /// <param name="publicAccessType">Public access. (default: Blob)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The container</returns>
        public async Task<BlobContainerClient> GetOrCreateContainerAsync(string containerName, PublicAccessType publicAccessType = PublicAccessType.Blob, CancellationToken cancellationToken = default)
        {
            var container = this.Container;
            if (container == null)
            {
                container = new BlobContainerClient(this.Options.ConnectionString, this.Options.GetContainerName(containerName));
            }

            await container.CreateIfNotExistsAsync(publicAccessType: publicAccessType, cancellationToken: cancellationToken).AnyContext();
            return container;
        }
    }
}
