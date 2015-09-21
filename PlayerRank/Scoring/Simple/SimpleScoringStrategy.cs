using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlayerRank.Scoring.Simple
{
    public class SimpleScoringStrategy : IScoringStrategy
    {
        public void Reset()
        {
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
                    player.AddPoints(result.Position.GetEquivalentPoints());
                }
                else
                {
                    player.AddPoints(result.Points);
                }
            }

            scoreboard = scoreboard.OrderByDescending(p => p.Points).ToList();

            for (var i = 0; i < scoreboard.Count; i++)
            {
                scoreboard[i].Position = new Position(i+1);
            }

            return scoreboard;
        }
    }
}