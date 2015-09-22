using System;
using System.Runtime.InteropServices;

namespace PlayerRank
{
    public class PlayerScore
    {
        public string Name { get; set; }
        public Points Points { get; internal set; }
        public Position Position { get; internal set; }

        public PlayerScore(string name)
        {
            Name = name;
            Points = new Points(0);
            Position = new Position(0);
        }

        public PlayerScore(string name, Position position) : this(name)
        {
            Position = position;
        }

        public PlayerScore(string name, Points points) : this(name)
        {
            Points = points;
        }

        internal void AddPoints(Points points)
        {
            Points += points;
        }

        internal void SubtractPoints(Points points)
        {
            Points -= points;
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