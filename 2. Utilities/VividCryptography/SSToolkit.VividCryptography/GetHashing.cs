namespace SSToolkit.VividCryptography
{
    public static partial class Extensions
    {
        public static string GetHashing(this string value, byte[]? salt = null, int iteration = 1,
            HashType hashType = HashType.Sha256)
            => Hashing.ComputeHash(value, salt, iteration, hashType);
    }
}
