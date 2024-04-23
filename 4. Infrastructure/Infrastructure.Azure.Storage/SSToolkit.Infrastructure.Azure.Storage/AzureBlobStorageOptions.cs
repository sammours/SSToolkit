namespace SSToolkit.Infrastructure.Azure.Storage
{
    using System;
    using SSToolkit.Fundamental.Extensions;

    public class AzureBlobStorageOptions
    {
        private const string Regex = "^(?=.{3,63}$)(?:[a-z0-9]+-?[a-z0-9]+)$";

        /// <summary>
        /// Gets or Sets connection string
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets container name (default: main)
        /// <para>
        /// <b>Valid container name: </b><br />
        /// Only small letters, numbers and - are allowed<br />
        /// Dashes preceded and followed by a letter or number <br />
        /// Must start or end with a letter or number <br />
        /// Min length: 3 and max length: 63 <br />
        /// </para>
        /// <br><see href="https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata"/></br>
        /// </summary>
        public string ContainerName { get; set; } = "main";

        public string GetContainerName(string containerName)
        {
            if (containerName.IsNotNullOrEmpty())
            {
                return GetContainerNameWhenValid(containerName);
            }

            if (this.ContainerName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(containerName));
            }

            return GetContainerNameWhenValid(this.ContainerName);
        }

        private static string GetContainerNameWhenValid(string containerName)
            => containerName.IsValidRegex(Regex)
                    ? containerName
                    : throw new ArgumentException("Container name is invalid. See: https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata", nameof(containerName));
    }
}
