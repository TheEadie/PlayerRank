using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ELORank
{
    public class EloScoringStrategy : IScoringStrategy
    {
        const double k = 32;

        public void NewPlayer(Player player)
        {
            player.Score = 1400;
        }

        public void UpdateScores(League league, Game game)
        {
            var results = game.GetResults();
            var previousScores = new Dictionary<string, double>();

            foreach (var playerName in results.Keys)
            {
                previousScores.Add(playerName, league.GetPlayer(playerName).Score);
            }

            foreach (var playerAName in results.Keys)
            {
                foreach (var playerBName in results.Keys)
                {
                    if (playerAName == playerBName) continue;
                    
                    var playerAResult = results[playerAName];
                    var playerBResult = results[playerBName];

                    var playerA = league.GetPlayer(playerAName);
                    var playerB = league.GetPlayer(playerBName);

                    var propabilityOfWinning = ExpectedValue(previousScores[playerAName], previousScores[playerBName]);
                    var ratingChange = RatingChange(propabilityOfWinning, (playerAResult > playerBResult));

                    playerA.AddScore(ratingChange);
                }
            }
        }

        private double RatingChange(double expectedValue, bool win)
        {
            var w = (win) ? 1 : 0;
            return k * (w - expectedValue);
        }

        private double ExpectedValue(double ratingA, double ratingB)
        {
            return 1 / (1 + Math.Pow(10.0, (ratingB - ratingA) / 400));
        }
    }
}