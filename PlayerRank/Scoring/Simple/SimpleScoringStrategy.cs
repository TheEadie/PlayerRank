using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring.Simple
{
    public class SimpleScoringStrategy : IScoringStrategy
    {
        private readonly IDictionary<Position, Points> m_PositionToPoints = new Dictionary<Position, Points>();

        public SimpleScoringStrategy()
        {
            m_PositionToPoints.Add(new Position(1), new Points(10));
            m_PositionToPoints.Add(new Position(2), new Points(9));
            m_PositionToPoints.Add(new Position(3), new Points(8));
            m_PositionToPoints.Add(new Position(4), new Points(7));
            m_PositionToPoints.Add(new Position(5), new Points(6));
            m_PositionToPoints.Add(new Position(6), new Points(5));
            m_PositionToPoints.Add(new Position(7), new Points(4));
            m_PositionToPoints.Add(new Position(8), new Points(3));
            m_PositionToPoints.Add(new Position(9), new Points(2));
            m_PositionToPoints.Add(new Position(10), new Points(1));
        }

        public SimpleScoringStrategy(IDictionary<Position, Points> pointsMap)
        {
            m_PositionToPoints = pointsMap;
        }

        public void Reset()
        {
        }

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
                    var pointsToAdd = m_PositionToPoints.ContainsKey(result.Position)
                        ? m_PositionToPoints[result.Position]
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