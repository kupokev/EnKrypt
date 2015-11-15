using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Ciphers.Duotrige
{
    public class Core
    {
        public Core()
        {
            Alphabet = CharacterSet;
            NumberBase = 32;
            Max = (NumberBase * DuotrigesimalSymbols.Length) - 1;
        }

        public readonly char[] CharacterSet = new char[] {
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '+', ',', '-', '.', '/',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            ':', ';', '<', '=', '>', '?', '@',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '[', '\\', ']', '^', '_', '`',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '{', '|', '}', '~'
        };

        protected char[] Alphabet;

        protected int Max;
        protected int NumberBase;

        protected readonly char[] _alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        protected readonly char[] DuotrigesimalSymbols = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V'
        };

        /// <summary>
        /// Removes any non numeric symbols and returns an Int32 with remaining numbers
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static int ConvertToNumeric32(string value)
        {
            var n = new string(value.Where(c => char.IsDigit(c)).ToArray());
            return Convert.ToInt32(n);
        }

        /// <summary>
        /// Removes any non numeric symbols and returns an Int64 with remaining numbers
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static Int64 ConvertToNumeric64(string value)
        {
            var n = new string(value.Where(c => char.IsDigit(c)).ToArray());
            return Convert.ToInt64(n);
        }

        /// <summary>
        /// Converts text to chunks of integers seperated by decimals.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alphabet"></param>
        /// <returns></returns>

        protected string ConvertToIntString(string text, char[] alphabet)
        {
            var value = "";
            var shelf = FillShelf(text, alphabet);

            foreach (var record in shelf)
            {
                value += record.ToString();

                // if (shelf.IndexOf(record) < shelf.Count) value += ".";
            }

            return value;
        }

        /// <summary>
        /// Breaks a string into a list of integers
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alphabet"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        protected static List<int> FillShelf(string text, char[] alphabet)
        {
            // TODO: Change to Int64
            var shelf = new List<int>();
            var index = 0;

            int record = 0;
            int length = 1;

            while (index + length <= text.Length)
            {
                int l = GetValue(text.Substring(index, length), alphabet);

                if (l < 0)
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

                if (index + length > text.Length)
                {
                    shelf.Add(record);
                    record = 0;
                }
            }

            return shelf;
        }

        /// <summary>
        /// Gets the numeric value of the text based off some crazy magic I need to figure out how I did.
        /// TODO: What Voodoo is this?
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        protected static int GetValue(string text, char[] alphabet)
        {
            // TODO: Change to Int64
            var textArray = text.ToArray();
            var value = 0;
            var i = 0;

            try
            {
                for (int j = textArray.Length; j > 0; j--)
                {
                    int index = Array.IndexOf(alphabet, textArray[i]);
                    value += Convert.ToInt32(Math.Pow(alphabet.Length, j - 1) * (index + 1));

                    i++;
                }

                return value;
            }
            catch
            {
                // Not actually an error.
                return -1;
            }
        }
    }
}
