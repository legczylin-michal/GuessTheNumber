using GuessTheNumber.menu;

namespace GuessTheNumber.nav
{
    public enum LeaderboardMenuOptions : int
    {
        EXIT
    }

    internal class LeaderboardMenu : IRunnable
    {
        public static void Run()
        {
            var menu = new Menu(5);

            menu.SetTitle("Leaderboard");

            Program._players.OrderBy(p => p.Value.BestScore?.Attempts);

            foreach (var player in Program._players)
            {
                menu.AddOption(player.Key, BDO.G(string.Format("{0} with best score {1}", player.Value.Name, player.Value.BestScore?.Attempts)));
            }

            menu.AddOption((int)LeaderboardMenuOptions.EXIT, BDO.G("exit"));

            menu.Run((key) =>
            {
                if (1 <= key && key <= Program._players.Count) Program._currentPlayer = key;
                else if (key == (int)LeaderboardMenuOptions.EXIT) return true;

                return false;
            });
        }
    }
}