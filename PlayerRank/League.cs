using System.Collections.Generic;
using System.Linq;
using PlayerRank.Scoring;

namespace PlayerRank
{
    public class League
    {
        private readonly List<Game> m_Games = new List<Game>(); 
        private readonly IList<Player> m_Players = new List<Player>();

        public IEnumerable<Player> GetLeaderBoard(IScoringStrategy scoringStrategy)
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