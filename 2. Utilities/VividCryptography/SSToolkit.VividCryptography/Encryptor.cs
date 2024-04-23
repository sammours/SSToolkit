namespace SSToolkit.VividCryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Encrypt and Decrypt texts
    /// </summary>
    public class Encryptor
    {
        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryptor"/> class.
        /// </summary>
        /// <param name="key">Encryption/Decryption key (Suggested: 64 or 128 bytes)</param>
        public Encryptor(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText">Plain text</param>
        /// <returns>Encrypted text</returns>
        public string Encrypt(string plainText)
        {
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("Plain text is null or empty");
            }

            if (this.key == null || this.key.Length <= 0)
            {
                throw new ArgumentNullException("Key is null or empty");
            }

            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            var cipherText = string.Empty;
            using (var encryptor = Aes.Create())
            {
                byte[] iV = new byte[15];
                var rand = new Random();
                rand.NextBytes(iV);
                var pdb = new Rfc2898DeriveBytes(this.key, iV);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }

                    cipherText = Convert.ToBase64String(iV) + Convert.ToBase64String(ms.ToArray());
                }
            }

            return cipherText;
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText">Cipher text</param>
        /// <returns>Plain text</returns>
        public string Decrypt(string cipherText)
        {
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("Cipher text is null or empty");
            }

            if (this.key == null || this.key.Length <= 0)
            {
                throw new ArgumentNullException("Key is null or empty");
            }

            byte[] iV = Convert.FromBase64String(cipherText.Substring(0, 20));
            byte[] cipherBytes = Convert.FromBase64String(cipherText.Substring(20).Replace(" ", "+"));
            var plainText = string.Empty;
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(this.key, iV);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }

                    plainText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return plainText;
        }
    }
}
