using System.Collections.Generic;
using System.Linq;

namespace ELORank
{
    public class League
    {
        private readonly IScoringStrategy m_ScoringStrategy;
        private readonly List<Player> m_Players = new List<Player>();

        public League(IScoringStrategy scoringStrategy)
        {
            m_ScoringStrategy = scoringStrategy;
        }

        public void AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            m_Players.Add(player);

            m_ScoringStrategy.NewPlayer(player);
        }

        public List<Player> GetLeaderBoard()
        {
            return m_Players.OrderBy(x => x.Score).ToList();
        }

        public void RecordGame(Game game)
        {
            m_ScoringStrategy.UpdateScores(this, game);
        }

        internal Player GetPlayer(string name)
        {
            return m_Players.SingleOrDefault(player => player.Name == name);
        }
    }
}