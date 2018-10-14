namespace EnKrypt.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class CubeUtility
    {
        /// <summary>
        /// Populate the 3D array with a string
        /// </summary>
        /// <param name="text">String to insert into array</param>
        /// <returns></returns>
        internal static char[,,] Fill(string text)
        {
            return Fill(text.ToCharArray());
        }

        /// <summary>
        /// Populate the 3D array with a string
        /// </summary>
        /// <param name="text">String to insert into array</param>
        /// <returns></returns>
        internal static char[,,] Fill(char[] text)
        {
            // Get the ceiling of the cube root of text length
            short length = Convert.ToInt16(Math.Ceiling(Math.Pow(text.Length, 1d / 3d)));

            // Set minimum to 3 for more security
            if (length < 3) length = 3;

            // Initialize new cube
            char[,,] cube = new char[length, length, length];

            // Set placeholder
            char ph = (char)Convert.ToInt32("00", 16);

            // To track where we are in the text string
            int index = 0;

            // Populate the array
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        // If we ran out of characters to populate, use the placeholder
                        if (index < text.Length)
                        {
                            cube[k, i, j] = text[index];
                            index++;
                        }
                        else
                        {
                            cube[k, i, j] = ph;
                        }
                    }
                }
            }

            return cube;
        }

        /// <summary>
        /// Populate the 3D array with a string in reverse order
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal static char[,,] FillReverse(string text)
        {
            short length = Convert.ToInt16(Math.Ceiling(Math.Pow(text.Length, 1d / 3d)));

            // Initialize new cube
            char[,,] cube = new char[length, length, length];

            // Calculate number of characters in cube
            int arrayLength = length * length * length;

            // Create new Character array 
            char[] cipher = new char[arrayLength];

            // To track where we are in the text string
            int index = 0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        cube[i, j, k] = text[index];
                        index++;
                    }
                }
            }

            return cube;
        }

        /// <summary>
        /// Converts a cube into a string
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        internal static char[] Flatten(char[,,] cube)
        {
            short length = Convert.ToInt16(Math.Pow(cube.Length, 1d / 3d));

            // Calculate number of characters in cube
            int arrayLength = length * length * length;

            // Create new Character array 
            char[] cipher = new char[arrayLength];

            // To track where we are in the text string
            int index = 0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        cipher[index] = cube[i, j, k];
                        index++;
                    }
                }
            }

            return cipher;
        }

        /// <summary>
        /// Converts a cube into a string in reverse order
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        internal static char[] FlattenReverse(char[,,] cube)
        {
            short length = Convert.ToInt16(Math.Pow(cube.Length, 1d / 3d));

            // Calculate number of characters in cube
            int arrayLength = length * length * length;

            // Create new Character array 
            char[] cipher = new char[arrayLength];

            // To track where we are in the text string
            int index = 0;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        cipher[index] = cube[k, i, j];
                        index++;
                    }
                }
            }

            int p = 0;

            for (int i = arrayLength - 1; i > 0; i--)
            {
                if (char.GetNumericValue(cipher[i]) != 0 && p == 0)
                {
                    p = i + 1;
                }
            }

            char[] retVal = new char[p];

            for (int i = 0; i < p; i++)
            {
                retVal[i] = cipher[i];
            }

            return retVal;
        }

        /// <summary>
        /// Reverse the rotation of a layer
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        internal static char[,,] ReverseLayer(char[,,] cube, int layer)
        {
            // Get cube column count
            short length = Convert.ToInt16(Math.Pow(cube.Length, 1d / 3d));

            // Get the actual layer number
            int[] layers = GetLayers(layer, length).Reverse().ToArray();

            foreach (var l in layers)
            {

                // To track where we are in the text string
                int index = 0;

                // Get layer Z
                char[] layerz = new char[length * length];

                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        layerz[index] = cube[i, j, l];
                        index++;
                    }
                }

                // Shift layer Z
                layerz = HexUtility.ShiftRight(layerz);

                // Replace layer Z
                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (index < layerz.Length)
                        {
                            cube[i, j, l] = layerz[index];
                            index++;
                        }
                    }
                }

                // Get layer Y
                char[] layery = new char[length * length];

                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        layery[index] = cube[i, l, j];
                        index++;
                    }
                }

                // Shift layer Y
                layery = HexUtility.ShiftRight(layery);

                // Replace layer Y
                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (index < layery.Length)
                        {
                            cube[i, l, j] = layery[index];
                            index++;
                        }
                    }
                }

                // Get layer X
                char[] layerx = new char[length * length];

                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        layerx[index] = cube[l, i, j];
                        index++;
                    }
                }

                // Shift layer X
                layerx = HexUtility.ShiftRight(layerx);

                // Replace layer X
                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (index <= layerx.Length)
                        {
                            cube[l, i, j] = layerx[index];
                            index++;
                        }
                    }
                }
            }

            return cube;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        internal static char[,,] RotateLayer(char[,,] cube, int layer)
        {
            // Get cube column count
            short length = Convert.ToInt16(Math.Pow(cube.Length, 1d / 3d));

            // Get the actual layer number
            int[] layers = GetLayers(layer, length);

            foreach (var l in layers)
            {
                // To track where we are in the text string
                int index = 0;

                // Get layer X
                char[] layerx = new char[length * length];

                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        layerx[index] = cube[l, i, j];
                        index++;
                    }
                }

                // Shift layer X
                layerx = HexUtility.ShiftLeft(layerx);

                // Replace layer X
                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        cube[l, i, j] = layerx[index];
                        index++;
                    }
                }

                // Get layer Y
                char[] layery = new char[length * length];

                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        layery[index] = cube[i, l, j];
                        index++;
                    }
                }

                // Shift layer Y
                layery = HexUtility.ShiftLeft(layery);

                // Replace layer Y
                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        cube[i, l, j] = layery[index];
                        index++;
                    }
                }

                // Get layer Z
                char[] layerz = new char[length * length];

                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        layerz[index] = cube[i, j, l];
                        index++;
                    }
                }

                // Shift layer Z
                layerz = HexUtility.ShiftLeft(layerz);

                // Replace layer Z
                index = 0;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        cube[i, j, l] = layerz[index];
                        index++;
                    }
                }
            }

            return cube;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal static int[] GetLayers(int layer, short length)
        {
            List<int> layers = new List<int>();

            // Avoid divide by 0
            if (layer > 0)
            {
                double m = Math.Ceiling((double)length / (double)layer);

                for (int i = 1; i < m; i++)
                {
                    if (layer * i <= length)
                    {
                        int l = layer * i;

                        if (l > 0) l = l - 1;

                        layers.Add(l);
                    }
                }
            }
            else
            {
                layers.Add(0);
            }

            return layers.ToArray();
        }
    }
}
