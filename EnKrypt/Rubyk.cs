using System.Text;

namespace EnKrypt
{
    public static class Rubyk
    {
        /// <summary>
        /// Converts ciphered text to plain text using an encoded key
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string value, byte[] key)
        {
            using (var cipher = new RubykCipher())
            {
                return cipher.Decrypt(value, key);
            }
        }

        /// <summary>
        /// Converts ciphered text to plain text using a plain text password
        /// </summary>
        /// <param name="value"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Decrypt(string value, string password)
        {
            using (var cipher = new RubykCipher())
            {
                return cipher.Decrypt(value, GetKey(password));
            }
        }

        /// <summary>
        /// Converts plain text to ciphered text using an encoded key
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string value, byte[] key)
        {
            using (var cipher = new RubykCipher())
            {
                return cipher.Encrypt(value, key);
            }
        }

        /// <summary>
        /// Converts plain text to ciphered text using a plain text password
        /// </summary>
        /// <param name="value"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Encrypt(string value, string password)
        {
            using (var cipher = new RubykCipher())
            {
                return cipher.Encrypt(value, GetKey(password));
            }
        }

        /// <summary>
        /// Converts a password to an encoded key
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static byte[] GetKey(string password)
        {
            return UTF8Encoding.Unicode.GetBytes(password);
        }
    }
}
