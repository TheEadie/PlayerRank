using System.Collections.Generic;
using System.Linq;
using PlayerRank.Scoring;

namespace PlayerRank
{
    public class League
    {
        private readonly List<Game> m_Games = new List<Game>(); 

        public IEnumerable<PlayerScore> GetLeaderBoard(IScoringStrategy scoringStrategy)
        {
            IList<PlayerScore> leaderBoard = new List<PlayerScore>();

            m_Games.Aggregate(leaderBoard, scoringStrategy.UpdateScores);

            return leaderBoard;
        }

        public void RecordGame(Game game)
        {
            m_Games.Add(game);
        }
    }
}