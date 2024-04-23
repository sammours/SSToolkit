# VividCryptography
VividCryptography: Encrypt/Decrypt and Hash

> Install-Package SSToolkit.VividCryptography

## Encryptor
- Encrypt and decrypt plain text using Aes algorithm and Rfc2898Derive to create random bytes.


<br>

## Hashing

- Compute a hash. (default: Sha256)
- Salt (default: null) (Suggested: 128bytes) and interation (default: 1) (Suggested: 100000) can be added to hashing
- Available algorithms (Md5, Sha1, Sha256, Sha384, Sha512)
- Create random salt bytes (default size: 64) (Suggested: 128)
- GetHashing string extension

<br>

## Usage

Encrypt/Decrypt
```csharp
var crypto = new Encryptor(your_key);
string plainText = "4242424242424242";
var cipherText = crypto.Encrypt(plainText);
var plainText = crypto.Decrypt(cipherText);
```

Hashing
```csharp
var salt = Hashing.GetSalt();
var hash = Hashing.ComputeHash("Hello world!");
var hashWithSalt = Hashing.ComputeHash("Hello world!", salt);
var hashWithIteration = Hashing.ComputeHash("Hello world!", salt, 100000);
var hashAlgorthirm = Hashing.ComputeHash("Hello world!", salt, 100000);
var hash = Hashing.ComputeHash("Hello world!", salt, 100000, HashType.Sha512);
```