using GuessTheNumber.menu;
using GuessTheNumber.saves;

namespace GuessTheNumber.nav
{
    public enum MainMenuOptions : int
    {
        PLAY, SELECT_PLAYER, LEADERBOARD, SETTINGS, EXIT
    }

    internal class MainMenu : IRunnable
    {
        public static void Run()
        {
            var menu = new Menu(5);

            menu.SetTitle("Guess The Number");

            menu.AddOption((int)MainMenuOptions.PLAY, "play");
            menu.AddOption((int)MainMenuOptions.SELECT_PLAYER, "select player");
            menu.AddOption((int)MainMenuOptions.LEADERBOARD, "leaderboard");
            menu.AddOption((int)MainMenuOptions.SETTINGS, "settings");
            menu.AddOption((int)MainMenuOptions.EXIT, "exit");

            menu.Run((key) =>
            {
                switch (key)
                {
                    case (int)MainMenuOptions.PLAY:
                        int attempts = Game.Play();
                        Program._players[Program._currentPlayer].Scores.Add(new Score { Attempts = attempts, Date = DateTime.Now });
                        break;
                    case (int)MainMenuOptions.SELECT_PLAYER:
                        break;
                    case (int)MainMenuOptions.LEADERBOARD:
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