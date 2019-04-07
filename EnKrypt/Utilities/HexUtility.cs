using System;

namespace EnKrypt
{
    internal static class HexUtility
    {
        /// <summary>
        /// Shift hex string to the left
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static char[] ShiftLeft(char[] value)
        {
            char[] n = new char[value.Length];
            
            string hexS = "";

            foreach (char b in value)
            {
                hexS += String.Format("{0:x2}", Convert.ToInt32(b));
            }

            byte[] ba = StringToByteArray(hexS);

            hexS = BitConverter.ToString(ba);

            string[] hexA = hexS.Split('-');

            if (hexA.Length > value.Length)
            {
                hexS = hexS.Substring(0, hexS.Length - 3);
                hexA = hexS.Split('-');
            }

            // Shift text
            hexS = string.Join("", hexA) + hexA[0];
            hexS = hexS.Substring(1, hexS.Length - 2);
            
            var array = StringToByteArray(hexS);

            for (int i = 0; i < n.Length; i++)
            {
                n[i] = Convert.ToChar(array[i]);
            }

            return n;
        }

        /// <summary>
        /// Shift hex string to the right
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static char[] ShiftRight(char[] value)
        {
            char[] n = new char[value.Length];

            // Shift Right
            string hexS = "";

            foreach (char b in value)
            {
                hexS += String.Format("{0:x2}", Convert.ToInt32(b));
            }

            byte[] ba = StringToByteArray(hexS);

            hexS = BitConverter.ToString(ba);

            string[] hexA = hexS.Split('-');

            hexS = hexA[hexA.Length - 1] + string.Join("", hexA);
            hexS = hexS.Substring(1, hexS.Length - 2);
            
            var array = StringToByteArray(hexS);

            for (int i = 0; i < n.Length; i++)
            {
                n[i] = Convert.ToChar(array[i]);
            }

            return n;
        }

        private static byte[] StringToByteArray(string value)
        {
            if (value.Length % 2 != 0)
            {
                value = "0" + value;
            }

            int NumberChars = value.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }
    }
}
