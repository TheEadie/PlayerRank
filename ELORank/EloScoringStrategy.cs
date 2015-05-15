using System;

namespace ELORank
{
    public class EloScoringStrategy : IScoringStrategy
    {
        public void UpdateScores(League league, Game game)
        {
            var results = game.GetResults();

            foreach (var playerAName in results.Keys)
            {
                foreach (var playerBName in results.Keys)
                {
                    if (playerAName == playerBName) continue;
                    
                    var playerAResult = results[playerAName];
                    var playerBResult = results[playerBName];

                    var playerA = league.GetPlayer(playerAName);
                    var playerB = league.GetPlayer(playerBName);

                    var propabilityOfWinning = ExpectedValue(playerA.Score, playerB.Score);
                    var ratingChange = RatingChange(propabilityOfWinning, (playerAResult > playerBResult));

                    playerA.AddScore(ratingChange);
                }
            }
        }

        private double RatingChange(double expectedValue, bool win)
        {
            var w = (win) ? 1 : 0;

            return 32*(w - expectedValue);
        }

        private double ExpectedValue(double ratingA, double ratingB)
        {
            return 1/(1 + Math.Pow(10.0, (ratingB - ratingA)/400));
        }
    }
}