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
        private readonly List<string> m_Players = new List<string>(); 

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

            var initialGame = new Game();

            foreach (var player in m_Players)
            {
                initialGame.AddResult(player, new Points(0));
            }

            var games = new List<Game> {initialGame};
            games.AddRange(m_Games);

            var history = new List<History>();
            IList<PlayerScore> leaderBoard = new List<PlayerScore>();

            foreach (var game in games)
            {
                scoringStrategy.UpdateScores(leaderBoard, game);
                scoringStrategy.SetPositions(leaderBoard);
                history.Add(new History(game, leaderBoard.Select(item => (PlayerScore)item.Clone()).ToList()));
            }

            return history;
        }

        /// <summary>
        /// Records a game in this league
        /// </summary>
        public void RecordGame(Game game)
        {
            m_Games.Add(game);

            foreach (var player in game.GetResults().Select(x => x.Name))
            {
                if (!m_Players.Contains(player))
                {
                    m_Players.Add(player);
                }
            }
        }
    }
}