using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using PlayerRank.Scoring;

namespace PlayerRank
{
    /// <summary>
    /// A league made up of multiple <see cref="Game"/>s over time.
    /// </summary>
    public class League
    {
        private readonly List<Game> m_Games = new List<Game>(); 

        /// <summary>
        /// Gets the current leader board for this league based on a specifed scoring strategy
        /// </summary>
        public IEnumerable<PlayerScore> GetLeaderBoard(IScoringStrategy scoringStrategy)
        {
            scoringStrategy.Reset();

            IList<PlayerScore> leaderBoard = new List<PlayerScore>();

            m_Games.Aggregate(leaderBoard, scoringStrategy.UpdateScores);

            scoringStrategy.SetPositions(leaderBoard);

            return leaderBoard;
        }

        public IEnumerable<History> GetLeaderBoardHistory(IScoringStrategy scoringStrategy)
        {
            scoringStrategy.Reset();

            var history = new List<History>();
            IList<PlayerScore> leaderBoard = new List<PlayerScore>();

            foreach (var game in m_Games)
            {
                scoringStrategy.UpdateScores(leaderBoard, game);
                scoringStrategy.SetPositions(leaderBoard);
                history.Add(new History(game, leaderBoard));
            }

            return history;
        }

        /// <summary>
        /// Records a game in this league
        /// </summary>
        public void RecordGame(Game game)
        {
            m_Games.Add(game);
        }
    }
}