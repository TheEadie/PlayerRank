using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRank
{
    /// <summary>
    /// A single game that has been played
    /// </summary>
    public class Game
    {
        private readonly ICollection<PlayerScore> m_Leaderboard = new List<PlayerScore>(); 

        /// <summary>
        /// Records the result of the given player based on <see cref="Points"/>.
        /// If the player already exists their points will be overridden.
        /// </summary>
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

        /// <summary>
        /// Records the result of the given player based on <see cref="Position"/>.
        /// If the player already exists their position will be overridden.
        /// </summary>
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

        /// <summary>
        /// Gets the results currently recorded for this game
        /// </summary>
        /// <returns></returns>
        internal ICollection<PlayerScore> GetResults()
        {
            return m_Leaderboard;
        }
    }
}