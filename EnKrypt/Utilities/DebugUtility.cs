namespace EnKrypt.Utilities
{
    using System;

    internal static class DebugUtility
    {
        private static bool _debug = true;

        /// <summary>
        /// Write a representation of the cube to the console
        /// </summary>
        /// <param name="cube"></param>
        internal static void DisplayCube(char[,,] cube)
        {
            try
            {
                // Try to get debug setting
                // to do later look for value in app.config

                short length = Convert.ToInt16(Math.Pow(cube.Length, 1d / 3d));

                int ai = 0;

                for (int i = 0; i < length; i++)
                {
                    char[] a = new char[length * length];
                    ai = 0;

                    if (_debug)
                    {
                        Console.WriteLine("Layer {0}:", i);
                    }

                    for (int j = 0; j < length; j++)
                    {
                        string row = "";

                        for (int k = 0; k < length; k++)
                        {
                            a[ai] = Char.IsLetterOrDigit(cube[i, j, k]) ? cube[i, j, k] : '0';
                            row += "  ";
                            row += a[ai].ToString();

                            ai++;
                        }

                        if (_debug)
                        {
                            Console.WriteLine(row);
                        }
                    }

                    if (_debug)
                    {
                        Console.WriteLine(Environment.NewLine);
                    }
                }
            }
            catch
            {

            }
        }

        internal static void DisplayCubeSize(char[,,] cube)
        {
            try
            {
                // Try to get debug setting
                // to do later look for value in app.config

                //Log if debug set to true
                if (_debug)
                {
                    short length = Convert.ToInt16(Math.Pow(cube.Length, 1d / 3d));

                    Console.WriteLine("Cube size {0}" + Environment.NewLine, length);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Write exception out to command line 
        /// </summary>
        /// <param name="e"></param>
        internal static void Log(Exception e)
        {
            try
            {
                // Try to get debug setting
                // to do later look for value in app.config

                //Log if debug set to true
                if (_debug)
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine("Message: {0}", e.Message);
                    Console.WriteLine("Stack Trace:");
                    Console.WriteLine(e.StackTrace.ToString());
                    Console.WriteLine("");
                }
            }
            catch
            {

            }
        }
    }
}
