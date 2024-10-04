using GuessTheNumber.nav;
using GuessTheNumber.saves;

namespace GuessTheNumber
{
    internal class Program
    {
        public static readonly Settings _settings;
        public static readonly Dictionary<int, Player> _players;
        public static int _currentPlayer;

        static Program()
        {
            _settings = Saves.GetSettings();
            _players = Saves.GetPlayers();
            _currentPlayer = Saves.GetLast();
        }

        static void Main(string[] args)
        {
            MainMenu.Run();
        }
    }
}