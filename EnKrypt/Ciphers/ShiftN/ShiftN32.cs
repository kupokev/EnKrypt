using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Ciphers.ShiftN
{
    public class ShiftN32 : Core
    {
        /// <summary>
        /// Applies the ShiftN32 cipher to the provided text.
        /// </summary>
        /// <param name="text">Text to apply cipher to</param>
        /// <returns></returns>
        public static string Cipher(string text)
        {
            var a = Core._alphabet;
            var p = Core._punctuation;
            return Cipher(text, a, p);
        }

        /// <summary>
        /// Applies the ShiftN32 cipher to the provided text.
        /// </summary>
        /// <param name="text">Text to apply cipher to</param>
        /// <param name="alphabet">Custom alphabet array</param>
        /// <returns></returns>
        public static string Cipher(string text, char[] alphabet)
        {
            var p = Core._punctuation;
            return Cipher(text, alphabet, p);
        }

        /// <summary>
        /// Applies the ShiftN32 cipher to the provided text.
        /// </summary>
        /// <param name="text">Text to apply cipher to</param>
        /// <param name="alphabet">Custom alphabet array</param>
        /// <param name="punctuation">Custom punctuation array</param>
        /// <returns></returns>
        public static string Cipher(string text, char[] alphabet, char[] punctuation)
        {
            var value = "";
            // Ensure all elements are lower case for comparison.
            // Add Chars for uppercase and punctuation annotation.
            var a = (new string(alphabet).ToLower() + "^$").ToArray(); 
            var shelf = FillShelf(text, a, punctuation);

            foreach (var record in shelf)
            {
                value += record.ToString();

                if (shelf.IndexOf(record) < shelf.Count) value += ".";
            }

            return value;
        }

        /// <summary>
        /// Breaks a string into a list of integers
        /// </summary>
        /// <param name="text"></param>
        /// <param name="a"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private static List<int> FillShelf(string text, char[] a, char[] p)
        {
            var shelf = new List<int>();
            var index = 0;

            int record = 0;
            int length = 1;

            while (index + length <= text.Length)
            {
                int l = GetValue(text.Substring(index, length), a, p);

                if(l < 0)
                {
                    shelf.Add(record);
                    record = 0;

                    index = index + length - 1;
                    length = 1;
                }
                else
                {
                    record = l;

                    length++;
                }

                if(index + length > text.Length)
                {
                    shelf.Add(record);
                    record = 0;
                }
            }

            return shelf;
        }

        private static int GetValue(string text, char[] a, char[] p)
        {
            var textArray = text.ToLower().ToArray();
            var value = 0;
            var i = 0;

            try
            {
                for (int j = textArray.Length; j > 0; j--)
                {
                    int index = Array.IndexOf(a, textArray[i]);
                    value += Convert.ToInt32(Math.Pow(a.Length, j - 1) * (index + 1));

                    i++;
                }

                return value;
            }
            catch (Exception e)
            {
                // Not actually an error.
                // Log(e);
                return -1;
            }
            
        }

        #region Decipher
        public static string Decipher(string value)
        {
            return Decipher(value, _alphabet);
        }

        public static string Decipher(string value, char[] alphabet)
        {
            var a = (new string(alphabet).ToLower() + "^$").ToArray(); 
            var parts = value.Split('.');
            var text = "";

            for (int i = 0; i < parts.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(parts[i]))
                {
                    text += DeCipher(Core.ToNumeric32(parts[i]), a);
                }
            }

            return text;
        }

        private static string DeCipher(int value, char[] alphabet)
        {
            if (value <= 0)
            {
                return "";
            }
            else if (value <= alphabet.Length)
            {
                return alphabet[value - 1].ToString();
            }
            else
            {
                var p = (value / alphabet.Length).ToString().Split('.');
                var p0 = Convert.ToInt32(p[0]);
                var p1 = value - (p0 * alphabet.Length);
                var plainText = "";

                if (p1 == 0)
                {
                    //plainText = DeCipher(p0 - 1, alphabet).ToString() + DeCipher(p1 + alphabet.Length, alphabet).ToString();
                    return DeCipher(p0 - 1, alphabet).ToString() + DeCipher(p1 + alphabet.Length, alphabet).ToString();
                }
                else
                {
                    //plainText = DeCipher(p0, alphabet).ToString() + DeCipher(p1, alphabet).ToString();
                    return DeCipher(p0, alphabet).ToString() + DeCipher(p1, alphabet).ToString();
                }

                //return plainText;
            }
        }
        #endregion Decipher
    }
}