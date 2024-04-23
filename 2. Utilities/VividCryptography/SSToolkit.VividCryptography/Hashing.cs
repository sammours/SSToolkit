namespace SSToolkit.VividCryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Generate and compare hashing
    /// </summary>
    public static class Hashing
    {
        /// <summary>
        /// Create a new salt
        /// </summary>
        /// <param name="saltSize">Salt size (default: 64) (Suggested: 128)</param>
        /// <returns>Salt bytes</returns>
        public static byte[] GetSalt(int saltSize = 64)
        {
                byte[] salt = new byte[saltSize];
                var cryptoProvider = RandomNumberGenerator.GetBytes(salt.Length);
                return cryptoProvider;
        }

        /// <summary>
        /// Compute hash string
        /// </summary>
        /// <param name="value">Plain text</param>
        /// <param name="salt">Salt that should be hashed with the value (default: null) (Suggested: 128bytes salt)</param>
        /// <param name="iteration">The hashing repeating count (default: 1) (Suggested: 100000)</param>
        /// <param name="hashType">The hashing type (default: 1)</param>
        /// <returns>Computed hash string</returns>
        public static string ComputeHash(string value, byte[]? salt = null, int iteration = 1, HashType hashType = HashType.Sha256)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var hash = value + (salt != null ? Convert.ToBase64String(salt) : string.Empty);
            for (var i = 0; i < iteration; i++)
            {
                using (var stream = ToStream(hash))
                {
                    using (var algorithm = CreateHashAlgorithm(hashType))
                    {
                        hash = Convert.ToBase64String(algorithm.ComputeHash(stream));
                    }
                }
            }

            return hash;
        }

        /// <summary>
        /// Compute hash bytes
        /// </summary>
        /// <param name="value">Plain byte array</param>
        /// <param name="salt">Salt that should be hashed with the value (default: null) (Suggested: 128bytes salt)</param>
        /// <param name="iteration">The hashing repeating count (default: 1) (Suggested: 100000)</param>
        /// <param name="hashType">The hashing type (default: 1)</param>
        /// <returns>Computed hash bytes</returns>
        public static byte[] ComputeHash(byte[] value, byte[]? salt = null, int iteration = 1, HashType hashType = HashType.Sha256)
        {
            if (value == null)
            {
                return Array.Empty<byte>();
            }

            return Convert.FromBase64String(ComputeHash(Convert.ToBase64String(value), salt, iteration, hashType));
        }

        private static Stream ToStream(string value)
        {
            if (value == null)
            {
                return new MemoryStream();
            }

            var stream = new MemoryStream();
            var sw = new StreamWriter(stream);
            sw.Write(value);
            sw.Flush();

            stream.Position = 0;
            return stream;
        }

        private static HashAlgorithm CreateHashAlgorithm(HashType hashType)
        {
            switch (hashType)
            {
                case HashType.Md5:
                    return MD5.Create();
                case HashType.Sha1:
                    return SHA1.Create();
                case HashType.Sha256:
                    return SHA256.Create();
                case HashType.Sha384:
                    return SHA384.Create();
                case HashType.Sha512:
                    return SHA512.Create();
                default:
                    throw new NotSupportedException($"{hashType} is an unsupported algorithm");
            }
        }
    }

#pragma warning disable SA1201 // Elements must appear in the correct order
    public enum HashType
#pragma warning restore SA1201 // Elements must appear in the correct order
    {
        Md5,
        Sha1,
        Sha256,
        Sha384,
        Sha512,
    }
}
