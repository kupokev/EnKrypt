using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Ciphers.Duotrige
{
    public class DecryptionManager : Core
    {
        /// <summary>
        /// Decrypts a cipher text using an alphabet character array
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        public string Decrypt(string text, char[] alphabet)
        {
            var toReturn = "";
            var value = ConvertBase32ToBase10(text);

            Alphabet = alphabet;
            
            foreach(var segment in SegmentIntString(value))
            {
                toReturn += ConvertFromIntToString(segment);
            }
            
            return toReturn;
        }

        private List<int> SegmentIntString(string text)
        {
            var toReturn = new List<int>();

            var complete = false;
            var index = 0;

            do
            {
                if (index >= text.Length) break;

                var length = int.MaxValue.ToString().Length;
                if (text.Length - index < length) length = text.Length - index;

                var value = GetIntValueFromString(text.Substring(index, length));
                toReturn.Add(value);

                length = value.ToString().Length;
                index = index + length;
            } while (!complete);

            return toReturn;
        }

        private int GetIntValueFromString(string text)
        {
            if (Convert.ToInt64(text) < int.MaxValue)
            {
                return Convert.ToInt32(text);
            }
            else
            {
                return Convert.ToInt32(text.Substring(0, text.Length - 1));
            }
        }

        private string ConvertFromIntToString(int value)
        {
            if (value <= 0)
            {
                return "";
            }
            else if (value <= Alphabet.Length)
            {
                return Alphabet[value - 1].ToString();
            }
            else
            {
                var p = (value / Alphabet.Length).ToString().Split('.');
                var p0 = Convert.ToInt32(p[0]);
                var p1 = value - (p0 * Alphabet.Length);

                if (p1 == 0)
                {
                    return ConvertFromIntToString(p0 - 1).ToString() + ConvertFromIntToString(p1 + Alphabet.Length).ToString();
                }
                else
                {
                    return ConvertFromIntToString(p0).ToString() + ConvertFromIntToString(p1).ToString();
                }
            }
        }

        private string ConvertBase32ToBase10(string cipheredString)
        {
            var toReturn = "";

            var charArray = cipheredString.ToArray();

            for (int i = 0; i < charArray.Length; i += 2)
            {
                toReturn = toReturn + ((Array.IndexOf(DuotrigesimalSymbols, charArray[i]) * DuotrigesimalSymbols.Length) +
                    Array.IndexOf(DuotrigesimalSymbols, charArray[i + 1])
                    ).ToString();
            }

            return toReturn;
        }
    }
}
