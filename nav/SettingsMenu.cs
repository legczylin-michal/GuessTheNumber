using GuessTheNumber.menu;
using GuessTheNumber.saves;

namespace GuessTheNumber.nav
{
    public enum SettingsMenuOptions : int
    {
        EDIT_MINIMUM, EDIT_MAXIMUM, EDIT_ABSOLUTE, EDIT_RADIUS, SAVE, DISCARD
    }

    internal class SettingsMenu : IRunnable
    {
        public static void Run()
        {
            var menu = new Menu(6);

            menu.SetTitle("Settings");

            menu.AddOption((int)SettingsMenuOptions.EDIT_MINIMUM, "edit minimum");
            menu.AddOption((int)SettingsMenuOptions.EDIT_MAXIMUM, "edit maximum");
            menu.AddOption((int)SettingsMenuOptions.EDIT_ABSOLUTE, "edit absolute");
            menu.AddOption((int)SettingsMenuOptions.EDIT_RADIUS, "edit radius");
            menu.AddOption((int)SettingsMenuOptions.SAVE, "save");
            menu.AddOption((int)SettingsMenuOptions.DISCARD, "discard");

            var settings = new Settings
            {
                Minimum = Program._settings.Minimum,
                Maximum = Program._settings.Maximum,
                Absolute = Program._settings.Absolute,
                Radius = Program._settings.Radius
            };

            menu.Run((key) =>
            {
                switch (key)
                {
                    case (int)SettingsMenuOptions.EDIT_MINIMUM:
                        EditMinimum(settings);
                        break;
                    case (int)SettingsMenuOptions.EDIT_MAXIMUM:
                        EditMaximum(settings);
                        break;
                    case (int)SettingsMenuOptions.EDIT_ABSOLUTE:
                        EditAbsolute(settings);
                        break;
                    case (int)SettingsMenuOptions.EDIT_RADIUS:
                        EditRadius(settings);
                        break;
                    case (int)SettingsMenuOptions.SAVE:
                        Program._settings.Minimum = settings.Minimum;
                        Program._settings.Maximum = settings.Maximum;
                        Program._settings.Absolute = settings.Absolute;
                        Program._settings.Radius = settings.Radius;
                        Saves.UpdateSettings(Program._settings);
                        return true;
                    case (int)SettingsMenuOptions.DISCARD:
                        return true;
                    default:
                        break;
                }

                return false;
            });
        }

        public static bool EditMinimum(Settings pSettings)
        {
            Console.WriteLine("original minimum is: {0}", Program._settings.Minimum);
            Console.WriteLine("latest minimum set: {0}", pSettings.Minimum);

            try
            {
                int newMinimum = Convert.ToInt32(Console.ReadLine());

                pSettings.Minimum = newMinimum;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EditMaximum(Settings pSettings)
        {
            Console.WriteLine("original maximum is: {0}", Program._settings.Maximum);
            Console.WriteLine("latest maximum set: {0}", pSettings.Maximum);

            try
            {
                int newMaximum = Convert.ToInt32(Console.ReadLine());

                pSettings.Maximum = newMaximum;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EditAbsolute(Settings pSettings)
        {
            Console.WriteLine("original absolute is: {0}", Program._settings.Absolute);
            Console.WriteLine("latest absolute set: {0}", pSettings.Absolute);

            try
            {
                bool newAbsolute = Convert.ToBoolean(Console.ReadLine());

                pSettings.Absolute = newAbsolute;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool EditRadius(Settings pSettings)
        {
            Console.WriteLine("original radius is: {0}", Program._settings.Radius);
            Console.WriteLine("latest radius set: {0}", pSettings.Radius);

            try
            {
                int newRadius = Convert.ToInt32(Console.ReadLine());

                pSettings.Radius = newRadius;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}