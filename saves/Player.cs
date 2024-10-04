namespace GuessTheNumber.saves
{
    internal class Score
    {
        public int Attempts { get; set; }
        public DateTime Date { get; set; }
    }

    internal class Player
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = "Guest";
        public List<Score> Scores { get; set; } = [];
        public DateTime? LastPlayed
        {
            get
            {
                if (Scores.Count == 0) return null;

                return Scores[^1].Date;
            }
        }
    }
}