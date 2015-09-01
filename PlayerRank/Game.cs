using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRank
{
    public class Game
    {
        private readonly ICollection<PlayerScore> m_Leaderboard = new List<PlayerScore>(); 

        public void AddResult(string name, Points points)
        {
            var player = m_Leaderboard.SingleOrDefault(x => x.Name == name);

            if (player != null)
            {
                player.Points = points;
            }
            else
            {
                m_Leaderboard.Add(new PlayerScore(name, points));
            }
        }

        public void AddResult(string name, Position position)
        {
            var player = m_Leaderboard.SingleOrDefault(x => x.Name == name);

            if (player != null)
            {
                player.Position = position;
            }
            else
            {
                m_Leaderboard.Add(new PlayerScore(name, position));
            }
        }

        internal ICollection<PlayerScore> GetResults()
        {
            return m_Leaderboard;
        }

        /// Obsolete V1 API

        [Obsolete("Please use AddResult(string, Points) instead")]
        public void AddResult(string name, double score)
        {
            AddResult(name, new Points(score));
        }
    }
}