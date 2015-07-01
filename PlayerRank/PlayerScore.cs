namespace PlayerRank
{
    public class PlayerScore
    {
        public string Name { get; set; }
        public double Score { get; internal set; }

        public PlayerScore(string name)
        {
            Name = name;
        }

        internal void AddScore(double score)
        {
            Score += score;
        }
    }
}