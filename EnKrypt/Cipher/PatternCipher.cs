using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace EnKrypt
{
    internal sealed class PatternCipher : IDisposable
    {
        public void Dispose()
        {
        }

        private static List<PatternOp> BuildSchedule(byte[] key, short length, byte[] nonce = null)
        {
            return PatternUtility.BuildOperations(key, length, nonce);
        }

        public string Encrypt(string value, byte[] key)
        {
            var cube = CubeUtility.Fill(value);
            short length = Convert.ToInt16(Math.Ceiling(Math.Pow(cube.Length, 1d / 3d)));

            // Generate per-message nonce (12 bytes)
            var nonce = new byte[12];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(nonce);
            }

            var schedule = BuildSchedule(key, length, nonce);

            // Apply schedule for a number of rounds equal to cube side length
            for (int round = 0; round < length; round++)
            {
                foreach (var op in schedule)
                {
                    cube = CubeUtility.ApplyAxisLayerShift(cube, op.Axis, op.Layer, op.Left, op.Count);
                }
            }

            // Flatten and base64-encode similar to RubykCipher
            byte[] data = Encoding.UTF8.GetBytes(new string(CubeUtility.Flatten(cube)));

            // Package as nonce || data and Base64-encode
            var payload = new byte[nonce.Length + data.Length];
            Buffer.BlockCopy(nonce, 0, payload, 0, nonce.Length);
            Buffer.BlockCopy(data, 0, payload, nonce.Length, data.Length);
            return Convert.ToBase64String(payload);
        }

        public string Decrypt(string value, byte[] key)
        {
            char[,,] cube;

            // Decode payload and extract nonce
            byte[] payload;
            try
            {
                payload = Convert.FromBase64String(value);
            }
            catch
            {
                throw new Exception("There was an error decoding the text");
            }

            if (payload.Length < 12)
            {
                throw new Exception("Invalid payload: missing nonce");
            }

            var nonceLen = 12;
            var nonce = new byte[nonceLen];
            Buffer.BlockCopy(payload, 0, nonce, 0, nonceLen);
            var dataLen = payload.Length - nonceLen;
            var data = new byte[dataLen];
            Buffer.BlockCopy(payload, nonceLen, data, 0, dataLen);

            var decoded = Encoding.UTF8.GetString(data);
            cube = CubeUtility.FillReverse(decoded);

            short length = Convert.ToInt16(Math.Ceiling(Math.Pow(cube.Length, 1d / 3d)));
            var schedule = BuildSchedule(key, length, nonce);

            // To invert, iterate rounds in reverse and invert each op's direction
            for (int round = length - 1; round >= 0; round--)
            {
                for (int i = schedule.Count - 1; i >= 0; i--)
                {
                    var op = schedule[i];
                    cube = CubeUtility.ApplyAxisLayerShift(cube, op.Axis, op.Layer, !op.Left, op.Count);
                }
            }

            return new string(CubeUtility.FlattenReverse(cube)).Replace('\0', ' ').Trim();
        }
    }
}
