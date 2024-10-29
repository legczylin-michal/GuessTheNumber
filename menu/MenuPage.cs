namespace GuessTheNumber.menu
{
    internal class MenuPage
    {
        private List<(int ID, IDynamicOption Option)> _options;
        private int _maxOptions;
        private int _current;

        public List<(int ID, IDynamicOption Option)> Options { get { return _options; } }

        public (int Index, int ID, IDynamicOption Option) Current { get { return (_current, _options[_current].ID, _options[_current].Option); } }

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

        public void AddOption(int pID, IDynamicOption pOption)
        {
            if (Full()) throw new Exception("Page is full");

            _options.Add((pID, pOption));
        }

        public void Up()
        {
            _current--;
            _current += _current < 0 ? _options.Count : 0;
        }

        public void Down()
        {
            _current++;
            _current %= _options.Count;
        }
    }
}