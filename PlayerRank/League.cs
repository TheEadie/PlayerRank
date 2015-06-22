using System.Collections.Generic;
using System.Linq;
using PlayerRank.Scoring;

namespace PlayerRank
{
    public class League
    {
        private readonly IScoringStrategy m_ScoringStrategy;
        private readonly List<Game> m_Games = new List<Game>(); 
        private readonly IList<Player> m_Players = new List<Player>();

        public League(IScoringStrategy scoringStrategy)
        {
            m_ScoringStrategy = scoringStrategy;
        }

        public IEnumerable<Player> GetLeaderBoard()
        {
            m_Players.Clear();

            m_Games.Aggregate(m_Players, (scoreboard, game) => m_ScoringStrategy.UpdateScores(scoreboard, game));

            return m_Players;
        }

        public void RecordGame(Game game)
        {
            m_Games.Add(game);
        }
    }
}