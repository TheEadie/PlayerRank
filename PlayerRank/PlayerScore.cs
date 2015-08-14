using System;
using System.Runtime.InteropServices;

namespace PlayerRank
{
    public class PlayerScore
    {
        public string Name { get; set; }
        public Points Points { get; private set; }
        
        public PlayerScore(string name)
        {
            Name = name;
            Points = new Points(0);
        }

        internal void AddPoints(Points points)
        {
            Points += points;
        }

        /// Obsolete V1 API

        [Obsolete("Please use Points instead")]
        public double Score
        {
            get { return Points.GetValue(); }
            internal set { Points = new Points(value); }
        }

    }
}