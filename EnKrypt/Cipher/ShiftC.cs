namespace EnKrypt.Cipher
{
    using EnKrypt.Utilities;
    using System;
    using System.Linq;

    public static class ShiftC
    {
        public static string Version = "1.0.0";

        public static string Cipher(string text, string password)
        {
            string retVal = "";

            // Get encryption Combination
            var combination = GetCombination(password);
            //var combination = new int[] { 1 };

            // Shift text in string by hex
            //text = new string(Shift.Left(text.ToCharArray()));
            //text = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));

            // Populate cube
            var cube = CubeUtility.Fill(text.ToCharArray());

            // Display cube in console
            DebugUtility.DisplayCube(cube);

            // Get length of cube
            short length = Convert.ToInt16(Math.Ceiling(Math.Pow(cube.Length, 1d / 3d)));

            DebugUtility.DisplayCubeSize(cube);

            // Shift cube based on combination
            for (int i = 0; i < length; i++)
            {
                foreach (var c in combination)
                {
                    cube = CubeUtility.RotateLayer(cube, c);
                }
            }

            // Display cube in console
            DebugUtility.DisplayCube(cube);

            // Flatten cube to string
            var flat = CubeUtility.Flatten(cube);

            // Shift text in string by hex
            retVal = new string(flat);

            return retVal;
        }

        public static string Decipher(string text, string password)
        {
            string retVal = "";

            // Get encryption Combination
            var combination = GetCombination(password).Reverse();
            //var combination = new int[] { 1 };

            // Populate cube
            var cube = CubeUtility.FillReverse(text);

            DebugUtility.DisplayCube(cube);

            // Get length of cube
            short length = Convert.ToInt16(Math.Ceiling(Math.Pow(cube.Length, 1d / 3d)));

            DebugUtility.DisplayCubeSize(cube);

            // Shift cube based on combination
            for (int i = 0; i < length; i++)
            {
                foreach (var c in combination)
                {
                    cube = CubeUtility.ReverseLayer(cube, c);
                }
            }

            // Flatten cube to string
            var flat = CubeUtility.FlattenReverse(cube);

            // Shift text in string by hex
            //retVal = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(new string(flat)));

            retVal = new string(flat);

            return retVal;
        }

        private static int[] GetCombination(string password)
        {
            // Convert password to int
            var stringValue = MathUtility.ConvertTextToInt(password);

            // Get square root of int
            var sqrt = MathUtility.GetSquareRoot(stringValue);

            // Drop the whole number
            var combination = (sqrt - Math.Floor(sqrt)).ToString().Replace("0.", "");

            // Create int array to return
            int[] retVal = new int[combination.Length];

            // Populate the array
            for (int i = 0; i < combination.Length; i++)
            {
                int j = 0;

                if (Int32.TryParse(combination.ElementAt(i).ToString(), out j))
                {
                    retVal[i] = j;
                }
            }

            // Return array
            return retVal;
        }
    }
}
