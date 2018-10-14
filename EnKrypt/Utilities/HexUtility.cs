namespace EnKrypt.Utilities
{
    using System;

    internal static class HexUtility
    {
        /// <summary>
        /// Shift hex string to the left
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        internal static char[] ShiftLeft(char[] a)
        {
            char[] n = new char[a.Length];

            //Console.WriteLine("Shift Left");

            string hexS = "";

            foreach (char b in a)
            {
                hexS += String.Format("{0:x2}", Convert.ToInt32(b));
            }

            byte[] ba = StringToByteArray(hexS);

            hexS = BitConverter.ToString(ba);

            string[] hexA = hexS.Split('-');

            if (hexA.Length > a.Length)
            {
                hexS = hexS.Substring(0, hexS.Length - 3);
                hexA = hexS.Split('-');
            }

            // Shift text
            hexS = string.Join("", hexA) + hexA[0];
            hexS = hexS.Substring(1, hexS.Length - 2);

            //Console.WriteLine(hexS);

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
        /// <param name="a"></param>
        /// <returns></returns>
        internal static char[] ShiftRight(char[] a)
        {
            char[] n = new char[a.Length];

            //Console.WriteLine("Shift Right");

            string hexS = "";

            foreach (char b in a)
            {
                hexS += String.Format("{0:x2}", Convert.ToInt32(b));
            }

            byte[] ba = StringToByteArray(hexS);

            hexS = BitConverter.ToString(ba);

            string[] hexA = hexS.Split('-');

            hexS = hexA[hexA.Length - 1] + string.Join("", hexA);
            hexS = hexS.Substring(1, hexS.Length - 2);

            //Console.WriteLine(hexS);

            var array = StringToByteArray(hexS);

            for (int i = 0; i < n.Length; i++)
            {
                n[i] = Convert.ToChar(array[i]);
            }

            return n;
        }

        /// <summary>
        /// Convert string to byte array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static byte[] StringToByteArray(String hex)
        {
            if (hex.Length % 2 != 0)
            {
                //hex = hex.Substring(0, hex.Length - 1);
                hex = "0" + hex;
            }

            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
