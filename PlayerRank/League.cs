using System.Collections.Generic;
using System.Linq;
using PlayerRank.Scoring;

namespace PlayerRank
{
    public class League
    {
        private readonly IScoringStrategy m_ScoringStrategy;
        private List<Player> m_Players = new List<Player>();
        private readonly List<Game> m_Games = new List<Game>(); 

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
            m_Players = new List<Player>();

            foreach (var game in m_Games)
            {
                m_ScoringStrategy.UpdateScores(this, game);
            }

            return m_Players.OrderBy(x => x.Score).ToList();
        }

        public void RecordGame(Game game)
        {
            m_Games.Add(game);
        }

        internal Player GetPlayer(string name)
        {
            return m_Players.SingleOrDefault(player => player.Name == name);
        }
    }
}