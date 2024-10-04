namespace GuessTheNumber.menu
{
    internal class MenuPage
    {
        private List<(int ID, string Name)> _options;
        private int _maxOptions;
        private int _current;

        public List<(int ID, string Name)> Options { get { return _options; } }

        public (int Index, int ID, string Name) Current { get { return (_current, _options[_current].ID, _options[_current].Name); } }

        public MenuPage(int pMaxOptions)
        {
            _options = [];
            _maxOptions = pMaxOptions;
            _current = 0;
        }

        public bool Full()
        {
            return _options.Count == _maxOptions;
        }

        public void AddOption(int pID, string pName)
        {
            if (Full()) throw new Exception("Page is full");

            _options.Add((pID, pName));
        }

        public void Up()
        {
            _current--;
            _current += _current < 0 ? _maxOptions : 0;
        }

        public void Down()
        {
            _current++;
            _current %= _maxOptions;
        }
    }
}