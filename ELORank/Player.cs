namespace ELORank
{
    public class Player
    {
        public string Name { get; set; }
        public double Score { get; private set; }

        public Player(string name)
        {
            Name = name;
        }

        internal void AddScore(double score)
        {
            Score += score;
        }

    }
}