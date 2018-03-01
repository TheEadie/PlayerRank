using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring.Simple
{
    public class SimpleScoringStrategy : IScoringStrategy
    {
        /// <summary>
        /// Maps each <see cref="Position"/> a player can finish to a number of <see cref="Points"/>
        /// </summary>
        private readonly IDictionary<Position, Points> _positionToPoints = new Dictionary<Position, Points>();

        /// <summary>
        /// Creates a new scoring strategy where each player scores points based on their position. 
        /// The player with the largest number of points is considered to be winning.
        /// 10 <see cref="Points"/> are awarded for 1st place down to 1 point for 10th.
        /// </summary>
        public SimpleScoringStrategy()
        {
            _positionToPoints.Add(new Position(1), new Points(10));
            _positionToPoints.Add(new Position(2), new Points(9));
            _positionToPoints.Add(new Position(3), new Points(8));
            _positionToPoints.Add(new Position(4), new Points(7));
            _positionToPoints.Add(new Position(5), new Points(6));
            _positionToPoints.Add(new Position(6), new Points(5));
            _positionToPoints.Add(new Position(7), new Points(4));
            _positionToPoints.Add(new Position(8), new Points(3));
            _positionToPoints.Add(new Position(9), new Points(2));
            _positionToPoints.Add(new Position(10), new Points(1));
        }

        /// <summary>
        /// Creates a new scoring strategy where each player scores points based on their position. 
        /// The player with the largest number of points is considered to be winning.
        /// <see cref="Points"/> are awarded based on the mapping provided.
        /// </summary>
        public SimpleScoringStrategy(IDictionary<Position, Points> pointsMap)
        {
            _positionToPoints = pointsMap;
        }
        
        /// <summary>
        /// Sets player's <see cref="Position"/>s based on who has the most points
        /// </summary>
        public void SetPositions(IList<PlayerScore> leaderBoard)
        {
            leaderBoard = leaderBoard.OrderByDescending(p => p.Points).ToList();

            for (var i = 0; i < leaderBoard.Count; i++)
            {
                var position = (i > 0 && leaderBoard[i].Points == leaderBoard[i - 1].Points)
                    ? new Position(i)
                    : new Position(i + 1);

                leaderBoard[i].Position = position;
            }
        }

        /// <summary>
        /// Updates the provided scoreboard with the results of a <see cref="Game"/>
        /// A player will gain the number of points specified or a number based on their
        /// position as specified in the constructor.
        /// </summary>
        public IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game)
        {
            foreach (var result in game.GetResults())
            {
                var player = scoreboard.SingleOrDefault(p => p.Name == result.Name);

                if (player == null)
                {
                    player = new PlayerScore(result.Name);
                    scoreboard.Add(player);
                    player.Points = new Points(0);
                    player.Position = new Position(0);
                }

                if (result.Points == new Points(0) &&
                    result.Position != new Position(0))
                {
                    var pointsToAdd = _positionToPoints.ContainsKey(result.Position)
                        ? _positionToPoints[result.Position]
                        : new Points(0);

                    player.AddPoints(pointsToAdd);
                }
                else
                {
                    player.AddPoints(result.Points);
                }
            }

            return scoreboard;
        }
    }
}