using System.Collections.Generic;
using System.Linq;

namespace EnKrypt.Ciphers.Duotrige
{
    public class Alphabet : Core
    {
        public char[] GenerateShortAlphabet(string value)
        {
            var alphabet = value.Distinct().OrderByDescending(c => c).ToArray();

            return alphabet.ToArray();
        }

        public char[] GenerateAlphabetFromPassword(string password)
        {
            // Step 1: Break password into character array
            // Step 2: Get group of characters for that character (Core.GetChars(char))
            // Step 3: Get distinct list of characters from the returned characters in step 2
            // Step 4: Get distinct list of characters not returned in step 2
            // Step 5: Build alphabet

            List<char> characterList = new List<char>();
            List<char> excludedCharacters = new List<char>();
            List<char> includedCharacters = new List<char>();

            // Step 1
            var characters = password.Distinct().OrderByDescending(c => c).ToArray();

            // Step 2
            foreach(var c in characters)
            {
                characterList.AddRange(GetChars(c));
            }

            // Step 3
            includedCharacters = characterList.Distinct().ToList();

            // Step 4
            excludedCharacters = CharacterSet.Where(x => !characterList.Contains(x)).ToList();

            // Step 5
            // TODO: Build Alphabet

            return characterList.ToArray();
        }
    }
}
