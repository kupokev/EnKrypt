using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnKrypt.Hash
{
    internal sealed class RubykHash : IDisposable
    {
        private readonly char[] _filler;
        private readonly byte[] _key;

        public RubykHash()
        {
            _filler = new char[30];
            _key = UTF8Encoding.Unicode.GetBytes("$!!enc!!hash!!v1.0!!!$");

            PopulaterFiller();
        }

        public void Dispose()
        {
        }

        public string GetHash(string value)
        {
            var newValue = new char[value.Length];

            if (value.Length < _filler.Length)
            {
                newValue = new char[_filler.Length * 2];

                var valueIndex = 0;

                for (int i = 0; i < newValue.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        if (i == 0)
                        {
                            newValue[i] = _filler[0];
                        }
                        else
                        {
                            newValue[i] = _filler[i / 2];
                        }
                    }
                    else
                    {
                        newValue[i] = (char)value.ToCharArray().GetValue(valueIndex++);

                        // Reset if at end of value string
                        if (valueIndex == value.Length)
                        {
                            valueIndex = 0;
                        }
                    }
                }
            }
            else
            {
                newValue = value.ToCharArray();
            }

            newValue = Rubyk.Encrypt(new string(newValue), _key).ToCharArray();

            byte[] data = Encoding.UTF8.GetBytes(ReduceArray(newValue));

            return Convert.ToBase64String(data);

            //return ReduceArray(newValue);
        }

        private void PopulaterFiller()
        {
            for (int i = 0; i < _filler.Length; i++)
            {
                _filler[i] = (char)(i * 2);
            }

            Array.Reverse(_filler);
        }

        private string ReduceArray(char[] value)
        {
            int maxLength = 24;
            int multiple = Math.Abs((int)Math.Floor((decimal)value.Length / (decimal)maxLength));

            var factors = new List<int>();
            var values = new List<char>();

            for (int i = 0; i < maxLength; i++)
            {
                for (int j = 0; j < multiple; j++)
                {
                    factors.Add(value[i * j]);
                }

                if (i % 2 == 0)
                {
                    values.Add((char)factors.Max());
                }
                else if(i % 3 == 0)
                {
                    values.Add(_filler[i]);
                }
                else
                {
                    values.Add((char)factors.Average());
                }
            }

            return new string(values.ToArray());
        }
    }
}