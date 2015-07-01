using System.Collections.Generic;
using System.Linq;
using PlayerRank.Scoring;

namespace PlayerRank
{
    public class League
    {
        private readonly List<Game> m_Games = new List<Game>(); 
        private readonly IList<PlayerScore> m_Players = new List<PlayerScore>();

        public IEnumerable<PlayerScore> GetLeaderBoard(IScoringStrategy scoringStrategy)
        {
            m_Players.Clear();

            m_Games.Aggregate(m_Players, scoringStrategy.UpdateScores);

            return m_Players;
        }

        public void RecordGame(Game game)
        {
            m_Games.Add(game);
        }
    }
}