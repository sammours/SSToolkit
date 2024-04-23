namespace SSToolkit.Infrastructure.Azure.Storage
{
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;

    public interface IAzureBlobStorage
    {
        /// <summary>
        /// Copy a source blob into the target (The source will not be deleted)
        /// </summary>
        /// <param name="source">The source blob name</param>
        /// <param name="target">The target blob name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options)</param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when copied</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="FileNotFoundException">FileNotFoundException</exception>
        Task<bool> CopyAsync(string source, string target, string containerName = "", CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a blob
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options)</param>
        /// <param name="deleteSnapshotsOption">(Default: Full deletion (IncludeSnapshots))</param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when deleted</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        Task<bool> DeleteAsync(string blobName, string containerName = "", DeleteSnapshotsOption deleteSnapshotsOption = DeleteSnapshotsOption.IncludeSnapshots, CancellationToken cancellationToken = default);

        /// <summary>
        /// Download to stream
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="stream">The stream to download the blob to</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options)</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        Task DownloadAsync(string blobName, Stream stream, string containerName = "", CancellationToken cancellationToken = default);

        /// <summary>
        /// Download to byte array
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The byte array</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        Task<byte[]> DownloadAsync(string blobName, string containerName = "", CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if blob does exist
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options)</param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when exists; otherwise, <c>false</c></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        Task<bool> ExistsAsync(string blobName, string containerName = "", CancellationToken cancellationToken = default);

        /// <summary>
        /// Rename a blob
        /// </summary>
        /// <param name="oldName">Old name</param>
        /// <param name="newName">New name</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options)</param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when renamed; otherwise, <c>false</c></returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        Task<bool> RenameAsync(string oldName, string newName, string containerName = "", CancellationToken cancellationToken = default);

        /// <summary>
        /// Upload a blob byte array
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="bytes">Byte array</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options)</param>
        /// <param name="shouldReplace"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when uploaded</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        Task<bool> UploadAsync(string blobName, byte[] bytes, string containerName = "", bool shouldReplace = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Upload a blob stream
        /// </summary>
        /// <param name="blobName">Blob name</param>
        /// <param name="stream">The stream</param>
        /// <param name="containerName">Container name (Optional, when null, upload to the container provided in Options)</param>
        /// <param name="shouldReplace"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when uploaded</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        Task<bool> UploadAsync(string blobName, Stream stream, string containerName = "", bool shouldReplace = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a container when found
        /// </summary>
        /// <param name="containerName">The container name</param>
        /// <param name="cancellationToken"></param>
        /// <returns><c>true</c> when deleted; otherwise, <c>false</c></returns>
        Task<bool> DeleteContainerAsync(string containerName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create or Get container by name
        /// </summary>
        /// <param name="containerName">The container name</param>
        /// <param name="publicAccessType">Public access. (default: Blob)</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The container</returns>
        Task<BlobContainerClient> GetOrCreateContainerAsync(string containerName, PublicAccessType publicAccessType = PublicAccessType.Blob, CancellationToken cancellationToken = default);
    }
}