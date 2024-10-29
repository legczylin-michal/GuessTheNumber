using System.Text;

namespace GuessTheNumber
{
    internal class WordGenerator
    {
        private static List<char> lowercase = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
        public static string Get(int pLength)
        {
            var result = new StringBuilder();

            for (int i = 0; i < pLength; i++)
            {
                var index = System.Security.Cryptography.RandomNumberGenerator.GetInt32(0, lowercase.Count);
                result.Append(lowercase[index]);
            }

            return result.ToString();
        }
    }
}