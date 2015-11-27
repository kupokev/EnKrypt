using System.Collections.Generic;

namespace EnKrypt.Ciphers.Duotrige
{
    public class Core
    {
        public Core()
        {
            Alphabet = CharacterSet;
            NumberBase = 32;
            Max = (NumberBase * DuotrigesimalSymbols.Length) - 1;
        }
        
        protected int Max;
        protected int NumberBase;

        protected readonly char[] _alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        protected char[] Alphabet;

        public readonly char[] CharacterSet = new char[] {
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '+', ',', '-', '.', '/',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            ':', ';', '<', '=', '>', '?', '@',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '[', '\\', ']', '^', '_', '`',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '{', '|', '}', '~'
        };

        protected readonly char[] DuotrigesimalSymbols = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V'
        };

        protected List<char> GetChars(char character)
        {
            var characters = new List<char>();

            switch(character)
            {
                case 'a':
                    characters.Add('K');
                    characters.Add('2');
                    characters.Add(';');
                    break;
                case 'b':
                    characters.Add('l');
                    characters.Add('O');
                    characters.Add('Q');
                    break;
                case 'c':
                    characters.Add('V');
                    characters.Add('D');
                    characters.Add('O');
                    break;
                case 'd':
                    characters.Add('q');
                    characters.Add('t');
                    break;
                case 'e':
                    characters.Add('D');
                    characters.Add('$');
                    characters.Add('-');
                    characters.Add('r');
                    break;
            }

            return characters;
        }
    }
}