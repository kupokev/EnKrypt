using System;
using System.Collections.Generic;

namespace EnKrypt
{
    internal enum Axis
    {
        X = 0,
        Y = 1,
        Z = 2
    }

    internal struct PatternOp
    {
        public int Axis;      // 0=X,1=Y,2=Z
        public int Layer;     // 0..(length-1)
        public bool Left;     // true=ShiftLeft, false=ShiftRight
        public int Count;     // number of nibble rotations
    }

    internal static class PatternUtility
    {
        /// <summary>
        /// Builds a deterministic operation schedule from the password-derived key and cube length.
        /// One password code unit can produce multiple operations across different axes and layers.
        /// </summary>
        /// <param name="key">Password key bytes (e.g., UTF-16LE from Rubyk.GetKey)</param>
        /// <param name="length">Cube side length</param>
        /// <param name="nonce">Optional per-message nonce to vary the schedule</param>
        /// <returns>List of operations to apply per round</returns>
        internal static List<PatternOp> BuildOperations(byte[] key, short length, byte[] nonce = null)
        {
            var ops = new List<PatternOp>();

            if (key == null || key.Length == 0 || length <= 0)
            {
                return ops;
            }

            // Combine bytes into 16-bit words to reduce many 0x00 effects for UTF-16LE
            for (int i = 0; i < key.Length; i += 2)
            {
                ushort word = 0;
                if (i + 1 < key.Length)
                {
                    word = (ushort)(key[i] | (key[i + 1] << 8));
                }
                else
                {
                    word = key[i];
                }

                // Mix in nonce byte (if provided) to vary schedule per message
                if (nonce != null && nonce.Length > 0)
                {
                    byte nb = nonce[(i / 2) % nonce.Length];
                    word = (ushort)(word ^ nb);
                }

                // Each word yields 3..7 operations (educational variability)
                int opCount = 3 + (word % 5);

                for (int t = 0; t < opCount; t++)
                {
                    // Re-mix per step with nonce to increase variability
                    ushort step = word;
                    if (nonce != null && nonce.Length > 0)
                    {
                        byte nb2 = nonce[(i / 2 + t) % nonce.Length];
                        step = (ushort)(step + nb2);
                    }

                    int axis = (step + t) % 3; // X/Y/Z
                    // Layer derived from shifting bits plus t to spread across layers
                    int layer = (int)(((step >> (t % 8)) + t) % length);
                    // Direction from adjacent bit
                    bool left = (((step >> ((t + 1) % 8)) & 0x1) == 0);
                    // Count between 1 and length
                    int count = 1 + ((step + t) % length);

                    ops.Add(new PatternOp
                    {
                        Axis = axis,
                        Layer = layer,
                        Left = left,
                        Count = count
                    });
                }
            }

            return ops;
        }
    }
}
