using System;

namespace EnKrypt.Common
{
    public class BaseConvert
    {
        public BaseConvert(int Base)
        {
            this.Max = (Base * hexSymbols.Length) - 1;
            this.NumberBase = Base;
        }

        public int Max;
        public readonly int NumberBase = 0;

        private char[] hexSymbols = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V' };

        public string ToTargetBase(int value)
        {
            int numBase = NumberBase;

            return CalculateValue(value, numBase);
        }

        private string CalculateValue(int value, int numBase)
        {   
            if (value > Max || value < 0) throw new ArgumentOutOfRangeException();

            var returnValue = "";

            int i = value;

            while (i != 0)
            {
                if (i < hexSymbols.Length)
                {
                    returnValue = returnValue + hexSymbols[i].ToString();
                    i = 0;
                }
                else
                {
                    int quotient = i / hexSymbols.Length;

                    if (quotient > hexSymbols.Length) quotient = hexSymbols.Length;

                    if (quotient > hexSymbols.Length - 1)
                    {
                        returnValue = returnValue + hexSymbols[hexSymbols.Length - 1].ToString();
                    }
                    else
                    {
                        returnValue = returnValue + hexSymbols[quotient].ToString();
                    }

                    if (i % hexSymbols.Length == 0)
                    {
                        returnValue = returnValue + "0";
                    }

                    i = i - (quotient * hexSymbols.Length);
                }
            }

            return returnValue;
        }
    }
}
