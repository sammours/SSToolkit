namespace SSToolkit.VividCryptography.Tests
{
    using SSToolkit.VividCryptography;
    using Xunit;

    public class HashingTests
    {
        [Fact]
        public void GetSalt_Test()
        {
            Assert.Equal(64, Hashing.GetSalt().Length);
            Assert.Equal(128, Hashing.GetSalt(128).Length);

            for (var i = 0; i < 1000; i++)
            {
                Assert.NotEqual(Hashing.GetSalt(), Hashing.GetSalt());
            }
        }

        [Fact]
        public void ComputeHash_Test()
        {
            // Arrange
            string plainText1 = "Hello world1";
            string plainText2 = "Hello world2";

            var plainBytes1 = new byte[] { 1, 2, 3 };
            var plainBytes2 = new byte[] { 4, 5, 6 };

            var salt = Hashing.GetSalt();

            // Act
            var stringHashing1 = Hashing.ComputeHash(plainText1);
            var stringHashing2 = Hashing.ComputeHash(plainText2);

            var stringHashingWithSalt1 = Hashing.ComputeHash(plainText1, salt);
            var stringHashingWithSalt2 = Hashing.ComputeHash(plainText2, salt);

            var stringHashingWithIteration1 = Hashing.ComputeHash(plainText1, iteration: 100000);
            var stringHashingWithIteration2 = Hashing.ComputeHash(plainText2, iteration: 100000);

            var bytesHashing1 = Hashing.ComputeHash(plainBytes1);
            var bytesHashing2 = Hashing.ComputeHash(plainBytes2);

            var bytesHashingWithSalt1 = Hashing.ComputeHash(plainBytes1, salt);
            var bytesHashingWithSalt2 = Hashing.ComputeHash(plainBytes2, salt);

            var bytesHashingWithIteration1 = Hashing.ComputeHash(plainBytes1, iteration: 100000);
            var bytesHashingWithIteration2 = Hashing.ComputeHash(plainBytes2, iteration: 100000);

            // Asserts
            Assert.NotNull(stringHashing1);
            Assert.NotNull(stringHashing2);
            Assert.NotNull(stringHashingWithSalt1);
            Assert.NotNull(stringHashingWithSalt2);
            Assert.NotNull(stringHashingWithIteration1);
            Assert.NotNull(stringHashingWithIteration2);

            Assert.NotEqual(stringHashing1, stringHashing2);
            Assert.NotEqual(stringHashingWithSalt1, stringHashingWithSalt2);
            Assert.NotEqual(stringHashingWithIteration1, stringHashingWithIteration2);

            Assert.NotNull(bytesHashing1);
            Assert.NotNull(bytesHashing2);
            Assert.NotNull(bytesHashingWithSalt1);
            Assert.NotNull(bytesHashingWithSalt2);
            Assert.NotNull(bytesHashingWithIteration1);
            Assert.NotNull(bytesHashingWithIteration2);

            Assert.NotEqual(bytesHashing1, bytesHashing2);
            Assert.NotEqual(bytesHashingWithSalt1, bytesHashingWithSalt2);
            Assert.NotEqual(bytesHashingWithIteration1, bytesHashingWithIteration2);
        }

        [Fact]
        public void TestExtensions()
        {
            // Act
            string plainText = "Hello world1";

            // Arrange
            var result1 = plainText.GetHashing();
            var result2 = plainText.GetHashing();

            // Asserts
            Assert.NotNull(result1);
            Assert.NotNull(result2);

            Assert.Equal(result1, result2);
        }
    }
}
