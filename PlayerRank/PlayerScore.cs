namespace PlayerRank
{
    public class PlayerScore
    {

        public string Name { get; set; }
        public Score Rating { get; private set; }

        public double Score
        {
            get { return Rating.GetValue(); }
            internal set { Rating = new Score(value); }
        }

        public PlayerScore(string name)
        {
            Name = name;
            Rating = new Score(0);
        }

        internal void AddScore(Score score)
        {
            Rating += score;
        }

        internal void AddScore(double score)
        {
            Score += score;
        }
    }
}