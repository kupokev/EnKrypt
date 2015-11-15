using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Ciphers.ShiftN
{
    public class ShiftN64 : Core
    {
        public static string Cipher(string text, bool useDefaultAlphabet)
        {
            var value = "";
            var a = new char[0];
            var p = new char[0];

            if (useDefaultAlphabet)
            {
                a = Core._alphabet;
                p = Core._punctuation;
            }
            else
            {
                //a = Core.NewAlphabet(value);
            }

            var shelf = new List<Int64>();
            
            // ... Concert here.

            foreach(var record in shelf)
            {
                value += record.ToString();

                if (shelf.IndexOf(record) < shelf.Count) value += ".";
            }

            return value;
        }

        public static string Decipher(string value)
        {
            return Decipher(value, _alphabet);
        }

        public static string Decipher(string value, char[] alphabet)
        {
            var parts = value.Split('.');
            var text = "";

            for (int i = 0; i < parts.Length; i++)
            {
                text += DeCipher(Core.ToNumeric64(parts[i]));
            }

            return text;
        }

        private static string DeCipher(int value)
        {
            return DeCipher(Convert.ToInt64(value), _alphabet);
        }

        private static string DeCipher(Int64 value)
        {
            return DeCipher(value, _alphabet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Max Value: 9,223,372,036,854,775,807</param>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        private static string DeCipher(Int64 value, char[] alphabet)
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
                var p0 = Convert.ToInt64(p[0]);
                var p1 = value - (p0 * alphabet.Length);

                if (p1 == 0)
                {
                    return DeCipher(p0 - 1, alphabet).ToString() + DeCipher(p1 + alphabet.Length, alphabet).ToString();
                }
                else
                {
                    return DeCipher(p0, alphabet).ToString() + DeCipher(p1, alphabet).ToString();
                }
            }
        }
    }
}