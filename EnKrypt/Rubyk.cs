using System.Text;

namespace EnKrypt
{
    public static class Rubyk
    {
        public static string Decrypt(string value, byte[] key)
        {
            using (var cipher = new RubykCipher())
            {
                return cipher.Decrypt(value, key);
            }
        }

        public static string Encrypt(string value, byte[] key)
        {
            using (var cipher = new RubykCipher())
            {
                return cipher.Encrypt(value, key);
            }
        }

        public static byte[] GetKey(string password)
        {
            return UTF8Encoding.Unicode.GetBytes(password);
        }
    }
}
