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
        /// Converts ciphered text to plain text using a plain text password.
        /// If useVariablePattern is true, uses the password-driven variable pattern scheduler.
        /// </summary>
        /// <param name="value">Cipher text</param>
        /// <param name="password">Password</param>
        /// <param name="useVariablePattern">When true, use PatternCipher; otherwise use classic RubykCipher.</param>
        /// <returns></returns>
        public static string Decrypt(string value, string password, bool useVariablePattern)
        {
            if (useVariablePattern)
            {
                using (var cipher = new PatternCipher())
                {
                    return cipher.Decrypt(value, GetKey(password));
                }
            }
            else
            {
                using (var cipher = new RubykCipher())
                {
                    return cipher.Decrypt(value, GetKey(password));
                }
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
        /// Converts plain text to ciphered text using a plain text password.
        /// If useVariablePattern is true, uses the password-driven variable pattern scheduler.
        /// </summary>
        /// <param name="value">Plain text</param>
        /// <param name="password">Password</param>
        /// <param name="useVariablePattern">When true, use PatternCipher; otherwise use classic RubykCipher.</param>
        /// <returns></returns>
        public static string Encrypt(string value, string password, bool useVariablePattern)
        {
            if (useVariablePattern)
            {
                using (var cipher = new PatternCipher())
                {
                    return cipher.Encrypt(value, GetKey(password));
                }
            }
            else
            {
                using (var cipher = new RubykCipher())
                {
                    return cipher.Encrypt(value, GetKey(password));
                }
            }
        }

        // Note: V2 methods replaced by overloads for a cleaner API.

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
