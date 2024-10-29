using GuessTheNumber.menu;
using GuessTheNumber.saves;
using System.Text;

namespace GuessTheNumber.nav
{
    public enum MainMenuOptions : int
    {
        PLAY, CREATE_PLAYER, LEADERBOARD, SETTINGS, EXIT
    }

    class MainMenuDynamicOption : IDynamicOption
    {
        public static MainMenuDynamicOption G()
        {
            return new MainMenuDynamicOption();
        }

        public string Evaluate()
        {
            var result = new StringBuilder();

            result.Append("play as '");
            result.Append(Program._players[Program._currentPlayer].Name);
            result.Append('\'');

            return result.ToString();
        }
    }

    internal class MainMenu : IRunnable
    {
        public static void Run()
        {
            var menu = new Menu(5);

            menu.SetTitle("Guess The Number");

            menu.AddOption((int)MainMenuOptions.PLAY, MainMenuDynamicOption.G());
            menu.AddOption((int)MainMenuOptions.CREATE_PLAYER, BDO.G("create player"));
            menu.AddOption((int)MainMenuOptions.LEADERBOARD, BDO.G("leaderboard"));
            menu.AddOption((int)MainMenuOptions.SETTINGS, BDO.G("settings"));
            menu.AddOption((int)MainMenuOptions.EXIT, BDO.G("exit"));

            menu.Run((key) =>
            {
                switch (key)
                {
                    case (int)MainMenuOptions.PLAY:
                        Console.Clear();
                        int attempts = Game.Play();
                        Program._players[Program._currentPlayer].Scores.Add(new Score { Attempts = attempts, Date = DateTime.Now });
                        break;
                    case (int)MainMenuOptions.CREATE_PLAYER:
                        Console.Clear();
                        Console.Write("Entern username (leaving empty yields random name): ");
                        string username = Console.ReadLine() ?? "";
                        username = username == string.Empty ? WordGenerator.Get(10) : username;
                        var newPlayer = new Player
                        {
                            ID = Program._players.Keys.Max() + 1,
                            Name = username,
                            Scores = []
                        };
                        Program._players.Add(newPlayer.ID, newPlayer);
                        Program._currentPlayer = newPlayer.ID;
                        break;
                    case (int)MainMenuOptions.LEADERBOARD:
                        LeaderboardMenu.Run();
                        break;
                    case (int)MainMenuOptions.SETTINGS:
                        SettingsMenu.Run();
                        break;
                    case (int)MainMenuOptions.EXIT:
                        Saves.UpdatePlayers(Program._players);
                        return true;
                    default:
                        break;
                }

                return false;
            });
        }
    }
}