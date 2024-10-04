using System.Text.Json;

namespace GuessTheNumber.saves
{
    internal class Saves
    {
        static Saves()
        {
            // game directory

            Directory.CreateDirectory(Constants.GAME_FOLDER_PATH);

            // settings

            string settingsPath = Path.Combine(Constants.GAME_FOLDER_PATH, Constants.SETTINGS_FILE_NAME);

            if (!File.Exists(settingsPath))
            {
                var defaultSettings = new Settings
                {
                    Minimum = -100,
                    Maximum = 100,
                    Absolute = true,
                    Radius = 10
                };

                File.AppendAllText(settingsPath, JsonSerializer.Serialize(defaultSettings));
            }

            // profiles list

            string profilesPath = Path.Combine(Constants.GAME_FOLDER_PATH, Constants.PROFILES_FILE_NAME);

            if (!File.Exists(profilesPath))
            {
                var profiles = new Dictionary<int, Player>();

                var guestProfile = new Player
                {
                    ID = Constants.GUEST_PROFILE_ID,
                    Name = "Guest",
                    Scores = []
                };

                profiles.Add(Constants.GUEST_PROFILE_ID, guestProfile);

                File.AppendAllText(profilesPath, JsonSerializer.Serialize(profiles));
            }
        }

        public static Settings GetSettings()
        {
            string settingsPath = Path.Combine(Constants.GAME_FOLDER_PATH, Constants.SETTINGS_FILE_NAME);

            string contents = File.ReadAllText(settingsPath);

            return JsonSerializer.Deserialize<Settings>(contents)!;
        }

        public static void UpdateSettings(Settings settings)
        {
            string settingsPath = Path.Combine(Constants.GAME_FOLDER_PATH, Constants.SETTINGS_FILE_NAME);

            File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));
        }

        public static Dictionary<int, Player> GetPlayers()
        {
            string profilesPath = Path.Combine(Constants.GAME_FOLDER_PATH, Constants.PROFILES_FILE_NAME);

            string contents = File.ReadAllText(profilesPath);

            return JsonSerializer.Deserialize<Dictionary<int, Player>>(contents)!;
        }

        public static int GetGuest()
        {
            return Constants.GUEST_PROFILE_ID;
        }

        public static int GetLast()
        {
            var players = GetPlayers();

            return players.OrderBy(p => p.Value.LastPlayed.HasValue).ThenBy(p => p.Value.LastPlayed).First().Key;
        }

        public static void UpdatePlayers(Dictionary<int, Player> players)
        {
            string profilesPath = Path.Combine(Constants.GAME_FOLDER_PATH, Constants.PROFILES_FILE_NAME);

            File.WriteAllText(profilesPath, JsonSerializer.Serialize(players));
        }
    }
}