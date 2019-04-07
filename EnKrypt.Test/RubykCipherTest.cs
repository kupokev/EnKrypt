using EnKrypt;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private byte[] _key;

        [SetUp]
        public void Setup()
        {
            _key = Rubyk.GetKey("Test1234");
        }

        [Test]
        public void FullCycleLongMessage()
        {
            var message = "To start off with we need to test that this can support variable length messages. It seems when it hits a certain length it tends to start having issues. ";
            message += "Adding more text. How are these passing? ";

            var cipher = Rubyk.Encrypt(message, _key);

            var result = Rubyk.Decrypt(cipher, _key);

            Assert.AreEqual(message.Trim(), result);
        }

        [Test]
        public void FullCycleShortMessage()
        {
            var message = "This is a test";

            var cipher = Rubyk.Encrypt(message, _key);

            var result = Rubyk.Decrypt(cipher, _key);

            Assert.AreEqual(message, result);
        }
    }
}