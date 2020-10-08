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
    }
}
