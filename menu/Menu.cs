using System.Text;

namespace GuessTheNumber.menu
{
    interface IDynamicOption
    {
        string Evaluate();
    }

    /// <summary>
    /// BasicDynamicOption
    /// </summary>
    internal class BDO : IDynamicOption
    {
        private string _option;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pOption"></param>
        public BDO(string pOption)
        {
            _option = pOption;
        }

        /// <summary>
        /// BasicDynamicOption.Generate
        /// </summary>
        /// <param name="pOption"></param>
        /// <returns></returns>
        public static BDO G(string pOption)
        {
            return new BDO(pOption);
        }

        public string Evaluate()
        {
            return _option;
        }
    }

    internal class Menu
    {
        private string _title;
        private List<MenuPage> _pages;
        private int _current;
        private int _maxOptions;

        public Menu(int pMaxOptions)
        {
            _title = string.Empty;
            _pages = [new MenuPage(pMaxOptions)];
            _current = 0;
            _maxOptions = pMaxOptions;
        }

        public void SetTitle(string pTitle)
        {
            _title = pTitle;
        }

        public void AddOption(int pID, IDynamicOption pOption)
        {
            if (_pages[^1].Full()) _pages.Add(new MenuPage(_maxOptions));

            _pages[^1].AddOption(pID, pOption);
        }

        public override string? ToString()
        {
            var result = new StringBuilder();

            var currentPage = _pages[_current];

            for (int i = 0; i < currentPage.Options.Count; i++)
            {
                result.Append(' ');
                if (i == currentPage.Current.Index) result.Append('o');
                else result.Append(' ');
                result.Append(' ');

                result.Append(currentPage.Options[i].Option.Evaluate());
                result.Append('\n');
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