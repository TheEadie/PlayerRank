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
            scoringStrategy.Reset();

            IList<PlayerScore> leaderBoard = new List<PlayerScore>();

            m_Games.Aggregate(leaderBoard, scoringStrategy.UpdateScores);

            leaderBoard = leaderBoard.OrderByDescending(p => p.Points).ToList();
            
            for (var i = 0; i < leaderBoard.Count; i++)
            {
                var position = (i > 0 && leaderBoard[i].Points == leaderBoard[i - 1].Points)
                    ? new Position(i)
                    : new Position(i + 1);

                leaderBoard[i].Position = position;
            }

            return leaderBoard;
        }

        public void RecordGame(Game game)
        {
            m_Games.Add(game);
        }
    }
}