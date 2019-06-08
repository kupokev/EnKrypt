using System;
using System.Text;

namespace EnKrypt
{
    internal sealed class RubykCipher : IDisposable
    {
        public void Dispose()
        {
        }

        public string Decrypt(string value, byte[] key)
        {
            char[,,] cube;

            // Reverse array
            Array.Reverse(key);

            try
            {
                // Decode to UTF8
                var decoded = Encoding.UTF8.GetString(
                        System.Convert.FromBase64String(value),
                        0,
                        System.Convert.FromBase64String(value).Length
                    );

                // Populate cube
                cube = CubeUtility.FillReverse(decoded);
            }
            catch
            {
                throw new Exception("There was an error decoding the text");
            }
            
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
            return new string(CubeUtility.FlattenReverse(cube)).Replace('\0', ' ').Trim();
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

            // Shift text in string by hex
            byte[] data = Encoding.UTF8.GetBytes(new string(CubeUtility.Flatten(cube)));

            return Convert.ToBase64String(data);
        }
    }
}
