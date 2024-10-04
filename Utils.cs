namespace GuessTheNumber
{
    internal class Utils
    {
        public static string Rep(string str, int times)
        {
            return string.Concat(Enumerable.Repeat(str, times));
        }
    }
}