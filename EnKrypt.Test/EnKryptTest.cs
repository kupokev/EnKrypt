using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnKrypt.Test
{
    [TestClass]
    public class EnKryptTest
    {
        [TestMethod]
        public void CheckOutputShortString()
        {
            string password = "Testing#158";
            string plainText = "This is a test string";

            // Generate key from password
            var key = Rubyk.GetKey(password);

            // Encrypt the message
            var cipherText = Rubyk.Encrypt(plainText, key);

            // Decipher the text
            var decipheredText = Rubyk.Decrypt(cipherText, key);

            // Check deciphered text against original plainText
            Assert.AreEqual(plainText, decipheredText, true);
        }

        [TestMethod]
        public void CheckOutputShortString_V2()
        {
            string password = "RubikPass!2025";
            string plainText = "Educational cube pattern test.";

            var cipherText = Rubyk.Encrypt(plainText, password, true);
            var decipheredText = Rubyk.Decrypt(cipherText, password, true);

            Assert.AreEqual(plainText, decipheredText, true);
        }

        [TestMethod]
        public void NonceProducesDifferentCiphertexts_V2()
        {
            string password = "RubikPass!2025";
            string plainText = "Same input, different outputs due to nonce.";

            var ct1 = Rubyk.Encrypt(plainText, password, true);
            var ct2 = Rubyk.Encrypt(plainText, password, true);

            // Ciphertexts should differ because nonce changes per encryption
            Assert.AreNotEqual(ct1, ct2, true);

            // Both should decrypt to the same plaintext
            var pt1 = Rubyk.Decrypt(ct1, password, true);
            var pt2 = Rubyk.Decrypt(ct2, password, true);

            Assert.AreEqual(plainText, pt1, true);
            Assert.AreEqual(plainText, pt2, true);
        }
    }
}
