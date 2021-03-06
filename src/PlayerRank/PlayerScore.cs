﻿namespace PlayerRank
{
    /// <summary>
    /// A single line on a scoreboard
    /// Stores a players name, points and position
    /// </summary>
    public class PlayerScore
    {
        public string Name { get; }
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

        public PlayerScore Clone()
        {
            return new PlayerScore(Name)
            {
                Points = Points,
                Position = Position
            };
        }
    }
}