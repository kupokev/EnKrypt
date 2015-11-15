using EnKrypt.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Ciphers.ShiftN
{
    public class Core : ErrorHandler
    {
        private static readonly char[] _hexSymbols = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V' };

        private static readonly int AlphabetLength = 36;
        private static readonly int NumberBase = 32;
        private static readonly int Max = (NumberBase * _hexSymbols.Length) - 1;

        protected static char[] _alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        //protected static char[] _alphabet = new char[] { '^', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        protected static char[] _punctuation = new char[] { '?', '!', '@', '#', '$', '%', '&', '*', '-', ':', ';', ' ', '.', ',', '~', '`', '^', '(', ')', '_', '=', '+', '[', ']', '{', ']', '\\', '|', '\'', '"', '<', '>', '/' };
        
        protected static int ToNumeric32(string value)
        {
            var n = new string(value.Where(c => char.IsDigit(c)).ToArray());
            return Convert.ToInt32(n);
        }

        protected static Int64 ToNumeric64(string value)
        {
            var n = new string(value.Where(c => char.IsDigit(c)).ToArray());
            return Convert.ToInt64(n);
        }


        public static char[] NewAlphabet(string value)
        {
            var alphabet = Cleanse(new string(value.ToLower().Distinct().OrderByDescending(c => c).ToArray()));
            
            return alphabet.ToArray();
        }

        public static string Condense(string cipheredString)
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
                        toConcat = toConcat + ToTargetBase(i);
                        completed = true;
                    }
                    else if (Convert.ToInt32(toCondense.Substring(0, maxNumberLength)) < Max)
                    {
                        int i = Convert.ToInt32(toCondense.Substring(0, maxNumberLength));
                        toConcat = toConcat + ToTargetBase(i);
                        toCondense = toCondense.Substring(maxNumberLength, toCondense.Length - maxNumberLength);
                    }
                    else
                    {
                        int i = Convert.ToInt32(toCondense.Substring(0, maxNumberLength - 1));
                        toConcat = toConcat + ToTargetBase(i);
                        toCondense = toCondense.Substring(maxNumberLength - 1, toCondense.Length - maxNumberLength - 1);
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
        /// Removes all non alphaNumeric symbols from a text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected static string Cleanse(string text)
        {
            char[] arr = text.Where(c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-')).ToArray();

            return new string(arr);
        }

        private static string ToTargetBase(int value)
        {
            int numBase = NumberBase;

            return CalculateValue(value, numBase);
        }

        private static string CalculateValue(int value, int numBase)
        {
            if (value > Max || value < 0) throw new ArgumentOutOfRangeException();

            var returnValue = "";

            int i = value;

            while (i != 0)
            {
                if (i < _hexSymbols.Length)
                {
                    returnValue = returnValue + _hexSymbols[i].ToString();
                    i = 0;
                }
                else
                {
                    int quotient = i / _hexSymbols.Length;

                    if (quotient > _hexSymbols.Length) quotient = _hexSymbols.Length;

                    if (quotient > _hexSymbols.Length - 1)
                    {
                        returnValue = returnValue + _hexSymbols[_hexSymbols.Length - 1].ToString();
                    }
                    else
                    {
                        returnValue = returnValue + _hexSymbols[quotient].ToString();
                    }

                    if (i % _hexSymbols.Length == 0)
                    {
                        returnValue = returnValue + "0";
                    }

                    i = i - (quotient * _hexSymbols.Length);
                }
            }

            return returnValue;
        }

        private static string FromTargetBase(string value)
        {
            int numBase = NumberBase;

            //return CalculateValue(value, numBase);
            return "";
        }
    }
}
