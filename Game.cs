namespace GuessTheNumber
{
    internal class Game
    {
        public static int Play()
        {
            int minimum = Program._settings.Minimum;
            int maximum = Program._settings.Maximum;
            bool absolute = Program._settings.Absolute;
            int radius = Program._settings.Radius;

            int answer = new Random().Next(minimum, maximum + 1);

            int attempts = 0;

            while (true)
            {
                try
                {
                    int guess = Convert.ToInt32(Console.ReadLine());

                    attempts++;

                    if (guess == answer) return attempts;

                    if (guess < minimum || guess > maximum)
                    {
                        Console.WriteLine("guess out of range. attempts won't get deducated");
                        attempts--;
                        continue;
                    }

                    if (Math.Abs(guess - answer) / (absolute ? 1 : (maximum - minimum) / 100) < radius) Console.WriteLine("You're near it");
                }
                catch (Exception)
                {
                    Console.WriteLine("invalid input. attempts won't get deducated");
                }
            }
        }
    }
}