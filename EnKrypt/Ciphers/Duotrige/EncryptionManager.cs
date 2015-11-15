using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Ciphers.Duotrige
{
    public class EncryptionManager : Core
    {
        public string Encrypt(string text, char[] alphabet)
        {
            var value = "";

            Alphabet = alphabet;

            value = ConvertToIntString(text, alphabet);

            Console.WriteLine("Int Value: " + value);

            value = ConvertBase10ToBase32(value);

            return value;
        }

        /// <summary>
        /// Reduces a Cipher String to a Duotrigesimal Base (32)
        /// </summary>
        /// <param name="cipheredString"></param>
        /// <returns></returns>
        private string ConvertBase10ToBase32(string cipheredString)
        {
            var returnValue = "";

            var cipheredStringArray = cipheredString.Split('.').Where(x => !String.IsNullOrWhiteSpace(x));

            int s = 1;

            var maxNumberLength = Max.ToString().Length;

            foreach (var cs in cipheredStringArray)
            {
                var completed = false;
                var toCondense = cs;
                var toConcat = "";

                do
                {
                    if (String.IsNullOrWhiteSpace(toCondense)) break;

                    if (toCondense.Length < maxNumberLength)
                    {
                        int i = Convert.ToInt32(toCondense);
                        toConcat = toConcat + CalculateValue(i, NumberBase);
                        completed = true;
                    }
                    else if (Convert.ToInt32(toCondense.Substring(0, maxNumberLength)) < Max)
                    {
                        int i = Convert.ToInt32(toCondense.Substring(0, maxNumberLength));
                        toConcat = toConcat + CalculateValue(i, NumberBase);
                        toCondense = toCondense.Substring(maxNumberLength, toCondense.Length - maxNumberLength);
                    }
                    else
                    {
                        int i = Convert.ToInt32(toCondense.Substring(0, maxNumberLength - 1));
                        toConcat = toConcat + CalculateValue(i, NumberBase);
                        toCondense = toCondense.Substring(maxNumberLength - 1, toCondense.Length - (maxNumberLength - 1));
                    }
                } while (!completed);

                if (s++ < cipheredStringArray.Count())
                {
                    returnValue = returnValue + "." + toConcat;
                }
                else
                {
                    returnValue = returnValue + toConcat;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numBase"></param>
        /// <returns></returns>
        private string CalculateValue(int value, int numBase)
        {
            if (value > Max || value < 0) throw new ArgumentOutOfRangeException();

            var returnValue = "";

            int i = value;

            if (i < DuotrigesimalSymbols.Length)
            {
                returnValue = "0" + DuotrigesimalSymbols[i].ToString();
            }
            else
            {
                while (i != 0)
                {
                    if (i < DuotrigesimalSymbols.Length)
                    {
                        returnValue = returnValue + DuotrigesimalSymbols[i].ToString();
                        i = 0;
                    }
                    else
                    {
                        int quotient = i / DuotrigesimalSymbols.Length;

                        if (quotient > DuotrigesimalSymbols.Length) quotient = DuotrigesimalSymbols.Length;

                        if (quotient > DuotrigesimalSymbols.Length - 1)
                        {
                            returnValue = returnValue + DuotrigesimalSymbols[DuotrigesimalSymbols.Length - 1].ToString();
                        }
                        else
                        {
                            returnValue = returnValue + DuotrigesimalSymbols[quotient].ToString();
                        }

                        if (i % DuotrigesimalSymbols.Length == 0)
                        {
                            returnValue = returnValue + "0";
                        }

                        i = i - (quotient * DuotrigesimalSymbols.Length);
                    }
                }
            }

            return returnValue;
        }
    }
}
