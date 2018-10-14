namespace EnKrypt.Utilities
{
    using System;
    using System.Linq;

    internal static class MathUtility
    {
        /// <summary>
        /// Converts characters in string to integers and then adds them up
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static int ConvertTextToInt(string text)
        {
            int retVal = 0;

            foreach (var v in text.ToArray())
            {
                var hex = Convert.ToByte(v).ToString("X2");

                var subTotal = 0;

                foreach (var h in hex.ToArray())
                {
                    int j = 0;

                    if (Int32.TryParse(h.ToString(), out j))
                    {
                        subTotal += j;
                    }
                    else
                    {
                        subTotal += ConvertTextToInt(h.ToString());
                    }
                }

                retVal += subTotal;
            }

            return retVal;
        }

        /// <summary>
        /// Get the first square root that is not a whole number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        internal static double GetSquareRoot(int number)
        {
            var retVal = Math.Sqrt(number);

            if ((retVal - Math.Floor(retVal)) == 0)
            {
                retVal = GetSquareRoot((int)retVal);
            }

            return retVal;
        }
    }
}
