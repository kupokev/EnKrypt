using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Ciphers.Duotrige
{
    public class Cipher
    {
        /// <summary>
        /// Encrypts a string using an alphabet character array.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        public string Encrypt(string text, char[] alphabet)
        {
            var manager = new EncryptionManager();

            return manager.Encrypt(text, alphabet);
        }

        /// <summary>
        /// Encrypts a string using a password to generate an alphabet character array.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Encrypt(string text, string password)
        {
            var alphabet = new Alphabet().GenerateAlphabet(password);

            var manager = new EncryptionManager();

            return manager.Encrypt(text, alphabet);
        }

        /// <summary>
        /// Decrypts a cipher text using an alphabet character array.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        public string Decrypt(string text, char[] alphabet)
        {
            var manager = new DecryptionManager();

            return manager.Decrypt(text, alphabet);
        }

        /// <summary>
        /// Decrypts a cipher text using a password to generate an alphabet character array.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Decrypt(string text, string password)
        {
            var alphabet = new Alphabet().GenerateAlphabet(password);

            var manager = new DecryptionManager();

            return manager.Decrypt(text, alphabet);
        }
    }
}
