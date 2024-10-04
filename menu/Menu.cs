using System.Text;

namespace GuessTheNumber.menu
{
    internal class Menu
    {
        private string _title;
        private List<MenuPage> _pages;
        private int _current;
        private int _maxOptions;
        private int _lengthOfTheLongestOption;

        public Menu(int pMaxOptions)
        {
            _title = string.Empty;
            _pages = [new MenuPage(pMaxOptions)];
            _current = 0;
            _maxOptions = pMaxOptions;
            _lengthOfTheLongestOption = 0;
        }

        public void SetTitle(string pTitle)
        {
            _title = pTitle;
        }

        public void AddOption(int pID, string pName)
        {
            if (_pages[^1].Full()) _pages.Add(new MenuPage(_maxOptions));

            _pages[^1].AddOption(pID, pName);

            if (_lengthOfTheLongestOption < pName.Length) _lengthOfTheLongestOption = pName.Length;
        }

        public override string? ToString()
        {
            int rightPadding = 1;
            int leftPadding = 1;

            int optionLineWidth = 1 + 1 + 1 + 1 + _lengthOfTheLongestOption;
            int width = optionLineWidth > _title.Length ? optionLineWidth : _title.Length;

            var result = new StringBuilder();

            var line = new StringBuilder();
            line.Append('+').Append(Utils.Rep("-", 3)).Append('+').Append(Utils.Rep("-", width - 3 + rightPadding)).Append('+').Append('\n');

            if (_title != string.Empty)
            {
                result.Append('+').Append(Utils.Rep("-", leftPadding + width + rightPadding)).Append('+').Append('\n');
                result.Append("| ").Append(_title).Append(Utils.Rep(" ", width - _title.Length)).Append(" |").Append('\n');
            }

            result.Append(line);

            var page = _pages[_current];

            for (int i = 0; i < page.Options.Count; i++)
            {
                var option = page.Options[i];
                result.Append("| ").Append(i == page.Current.Index ? "o" : " ").Append(" | ").Append(option.Name).Append(Utils.Rep(" ", width - 4 - option.Name.Length)).Append(" |").Append('\n');
                result.Append(line);
            }

            return result.ToString();
        }

        public void Run(Func<int, bool> func)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(this);

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        if (func(_pages[_current].Current.ID)) return;
                        break;
                    case ConsoleKey.UpArrow:
                        _pages[_current].Up();
                        break;
                    case ConsoleKey.DownArrow:
                        _pages[_current].Down();
                        break;
                    case ConsoleKey.LeftArrow:
                        _current--;
                        _current += _current < 0 ? _pages.Count : 0;
                        break;
                    case ConsoleKey.RightArrow:
                        _current++;
                        _current %= _pages.Count;
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }
    }
}