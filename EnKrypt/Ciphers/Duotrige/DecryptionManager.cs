using System;
using System.Linq;

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

            Alphabet = alphabet;

            foreach (var segment in text.Split('.'))
            {
                if (!String.IsNullOrWhiteSpace(segment))
                {
                    var value = Convert.ToInt32(ConvertBase32ToBase10(segment));
                    toReturn += ConvertFromIntToString(value);
                }
            }
            
            return toReturn;
        }

        private string ConvertFromIntToString(int value)
        {
            if (value < 0)
            {
                return "";
            }
            else if( value == 0)
            {
                return "0";
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
