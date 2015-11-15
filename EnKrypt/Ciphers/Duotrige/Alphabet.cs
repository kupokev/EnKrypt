using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnKrypt.Ciphers.Duotrige
{
    public class Alphabet
    {

        public char[] GenerateAlphabet(string value)
        {
            var alphabet = value.Distinct().OrderByDescending(c => c).ToArray();

            return alphabet.ToArray();
        }
    }
}
