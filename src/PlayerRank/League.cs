using System.Collections.Generic;
using System.Linq;
using PlayerRank.Scoring;

namespace PlayerRank
{
    /// <summary>
    /// A league made up of multiple <see cref="Game"/>s over time.
    /// </summary>
    public class League
    {
        private readonly List<Game> _games = new List<Game>(); 
        private readonly List<string> _players = new List<string>(); 

        /// <summary>
        /// Gets the current leader board for this league based on a specifed scoring strategy
        /// </summary>
        public IEnumerable<PlayerScore> GetLeaderBoard(IScoringStrategy scoringStrategy)
        {
            scoringStrategy.Reset();

            IList<PlayerScore> leaderBoard = new List<PlayerScore>();

            leaderBoard = _games.Aggregate(leaderBoard, scoringStrategy.UpdateScores);

            scoringStrategy.SetPositions(leaderBoard);

            return leaderBoard;
        }

        public IEnumerable<History> GetLeaderBoardHistory(IScoringStrategy scoringStrategy)
        {
            scoringStrategy.Reset();

            var initialGame = new Game();

            foreach (var player in _players)
            {
                initialGame.AddResult(player, new Points(0));
            }

            var games = new List<Game> {initialGame};
            games.AddRange(_games);

            var history = new List<History>();
            IList<PlayerScore> leaderBoard = new List<PlayerScore>();

            foreach (var game in games)
            {
                scoringStrategy.UpdateScores(leaderBoard, game);
                scoringStrategy.SetPositions(leaderBoard);
                history.Add(new History(game, leaderBoard.Select(item => item.Clone()).ToList()));
            }

            return history;
        }

        /// <summary>
        /// Records a game in this league
        /// </summary>
        public void RecordGame(Game game)
        {
            _games.Add(game);

            foreach (var player in game.GetResults().Select(x => x.Name))
            {
                if (!_players.Contains(player))
                {
                    _players.Add(player);
                }
            }
        }
    }
}