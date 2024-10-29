using GuessTheNumber.menu;
using GuessTheNumber.saves;
using System.Text;

namespace GuessTheNumber.nav
{
    public enum SettingsMenuOptions : int
    {
        EDIT_MINIMUM, EDIT_MAXIMUM, EDIT_ABSOLUTE, EDIT_RADIUS, SAVE, DISCARD
    }

    class SettingsMenuDynamicOption : IDynamicOption
    {
        private string _basic;
        private string _current;
        private string? _new;

        public SettingsMenuDynamicOption(string pBasic, string pCurrent)
        {
            _basic = pBasic;
            _current = pCurrent;
        }

        public void SetCurrent(string pCurrent)
        {
            _current = pCurrent;
        }

        public void SetNew(string pNew)
        {
            _new = pNew;
        }

        public string Evaluate()
        {
            var result = new StringBuilder();

            result.Append(_basic);
            result.Append(" [");
            result.Append(_current);

            if (!string.IsNullOrEmpty(_new))
            {
                result.Append(" => ");
                result.Append(_new);
            }

            result.Append(']');

            return result.ToString();
        }
    }

    internal class SettingsMenu : IRunnable
    {
        public static void Run()
        {
            // buffer for containing settings' changes instead of applying them immediately
            var settingsBuffer = new Settings
            {
                Minimum = Program._settings.Minimum,
                Maximum = Program._settings.Maximum,
                Absolute = Program._settings.Absolute,
                Radius = Program._settings.Radius
            };

            // generating object to contain menu entries
            // used in order to guarantee dynamical menu i.e. changes in text are reflected
            var editMinimum = new SettingsMenuDynamicOption("edit minimum", string.Format("{0}", settingsBuffer.Minimum));
            var editMaximum = new SettingsMenuDynamicOption("edit maximum", string.Format("{0}", settingsBuffer.Maximum));
            var editAbsolute = new SettingsMenuDynamicOption("edit absolute", string.Format("{0}", settingsBuffer.Absolute));
            var editRadius = new SettingsMenuDynamicOption("edit radius", string.Format("{0}", settingsBuffer.Radius));

            // creating menu
            var menu = new Menu(6);
            // setting up title
            menu.SetTitle("Settings");
            // adding options
            menu.AddOption((int)SettingsMenuOptions.EDIT_MINIMUM, editMinimum);
            menu.AddOption((int)SettingsMenuOptions.EDIT_MAXIMUM, editMaximum);
            menu.AddOption((int)SettingsMenuOptions.EDIT_ABSOLUTE, editAbsolute);
            menu.AddOption((int)SettingsMenuOptions.EDIT_RADIUS, editRadius);
            menu.AddOption((int)SettingsMenuOptions.SAVE, BDO.G("save"));
            menu.AddOption((int)SettingsMenuOptions.DISCARD, BDO.G("discard"));
            // running menu
            menu.Run((key) =>
            {
                switch (key)
                {
                    case (int)SettingsMenuOptions.EDIT_MINIMUM:
                        // read new value
                        EditMinimum(settingsBuffer);
                        // update menu entry
                        editMinimum.SetNew(string.Format("{0}", settingsBuffer.Minimum));
                        break;
                    case (int)SettingsMenuOptions.EDIT_MAXIMUM:
                        // read new value
                        EditMaximum(settingsBuffer);
                        // update menu entry
                        editMaximum.SetNew(string.Format("{0}", settingsBuffer.Maximum));
                        break;
                    case (int)SettingsMenuOptions.EDIT_ABSOLUTE:
                        // read new value
                        EditAbsolute(settingsBuffer);
                        // update menu entry
                        editAbsolute.SetNew(string.Format("{0}", settingsBuffer.Absolute));
                        break;
                    case (int)SettingsMenuOptions.EDIT_RADIUS:
                        // read new value
                        EditRadius(settingsBuffer);
                        // update menu entry
                        editRadius.SetNew(string.Format("{0}", settingsBuffer.Radius));
                        break;
                    case (int)SettingsMenuOptions.SAVE:
                        // apply buffered changes
                        Program._settings.Minimum = settingsBuffer.Minimum;
                        Program._settings.Maximum = settingsBuffer.Maximum;
                        Program._settings.Absolute = settingsBuffer.Absolute;
                        Program._settings.Radius = settingsBuffer.Radius;
                        // update settings file
                        Saves.UpdateSettings(Program._settings);
                        // exit
                        return true;
                    case (int)SettingsMenuOptions.DISCARD:
                        // exit
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