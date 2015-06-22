using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring
{
    public class SimpleScoringStrategy : IScoringStrategy
    {
        public IList<Player> UpdateScores(IList<Player> scoreboard, Game game)
        {
            foreach (var result in game.GetResults())
            {
                var player = scoreboard.SingleOrDefault(p => p.Name == result.Key);

                if (player == null)
                {
                    player = new Player(result.Key);
                    scoreboard.Add(player);
                    player.Score = 0;
                }
                
                player.AddScore(result.Value);
            }

            return scoreboard;
        }
    }
}