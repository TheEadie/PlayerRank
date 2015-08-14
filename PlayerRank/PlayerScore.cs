using System;

namespace PlayerRank
{
    public class PlayerScore
    {
        public string Name { get; set; }
        public double Score { get; internal set; }
        public Score Rating { get; private set; }

        public PlayerScore(string name)
        {
            Name = name;
        }

        internal void AddScore(Score score)
        {
            
        }

        internal void AddScore(double score)
        {
            Score += score;
        }
    }
}