using System;

namespace EnKrypt
{
    internal sealed class RubykCipher : IDisposable
    {
        public void Dispose()
        {
        }

        public string Decrypt(string value, byte[] key)
        {
            // Reverse array
            Array.Reverse(key);

            // Populate cube
            var cube = CubeUtility.FillReverse(value);
            
            // Get length of cube
            short length = Convert.ToInt16(Math.Ceiling(Math.Pow(cube.Length, 1d / 3d)));

            // Shift cube based on combination
            for (int i = 0; i < length; i++)
            {
                foreach (var k in key)
                {
                    cube = CubeUtility.ReverseLayer(cube, k);
                }
            }

            // Flatten cube to string
            var flat = CubeUtility.FlattenReverse(cube);
            
            var retVal = new string(flat);
            
            return retVal.Replace('\0', ' ').Trim();
        }

        public string Encrypt(string value, byte[] key)
        {
            // Populate cube
            var cube = CubeUtility.Fill(value);

            // Get length of cube
            short length = Convert.ToInt16(Math.Ceiling(Math.Pow(cube.Length, 1d / 3d)));
            
            // Shift cube based on combination
            for (int i = 0; i < length; i++)
            {
                foreach (var k in key)
                {
                    cube = CubeUtility.RotateLayer(cube, k);
                }
            }

            // Flatten cube to string
            var flat = CubeUtility.Flatten(cube);

            // Shift text in string by hex
            var retVal = new string(flat);

            return retVal;
        }
    }
}
