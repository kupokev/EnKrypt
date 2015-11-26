using System;
using System.Collections.Generic;
using System.Linq;

namespace EnKrypt.Ciphers.Duotrige
{
    public class EncryptionManager : Core
    {
        public string Encrypt(string text, char[] alphabet)
        {
            Alphabet = alphabet;

            var strings = ConvertToIntString(text, alphabet);

            Console.WriteLine("Int Value: ");

            var value = "";

            foreach(var s in strings)
            {
                Console.WriteLine(s.ToString());
                value += ConvertBase10ToBase32(s);

                if ((strings.IndexOf(s) + 1) < strings.Count) value += ".";
            }
            

            return value;
        }

        /// <summary>
        /// Converts text to chunks of integers seperated by decimals.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        private List<string> ConvertToIntString(string text, char[] alphabet)
        {
            var value = new List<string>();
            var shelf = FillShelf(text, alphabet);

            foreach (var record in shelf)
            {
                value.Add(record.ToString());
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
        private static List<int> FillShelf(string text, char[] alphabet)
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
        private static int GetValue(string text, char[] alphabet)
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

                    if (Convert.ToInt32(toCondense.Substring(0, 1)) == 0)
                    {
                        int i = 0;

                        toConcat = toConcat + CalculateValue(i, NumberBase);
                        toCondense = toCondense.Substring(1, toCondense.Length - 1);
                    }
                    else if (toCondense.Length < maxNumberLength)
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
