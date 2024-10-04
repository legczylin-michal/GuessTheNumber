namespace GuessTheNumber
{
    internal class Constants
    {
        public static string MY_DOCUMENTS_FOLDER_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string GAME_FOLDER_NAME = "GuessTheNumber";
        public static string GAME_FOLDER_PATH = Path.Combine(MY_DOCUMENTS_FOLDER_PATH, GAME_FOLDER_NAME);

        public static string SETTINGS_FILE_NAME = "settings.json";
        public static string PROFILES_FILE_NAME = "profiles.json";

        public static int GUEST_PROFILE_ID = 0;
    }
}
