using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring
{
    public class EloScoringStrategy : IScoringStrategy
    {
        /// <summary> also known as K in the ELO formula - the max change in rating from one game </summary>
        private const double ratingChangeBaseMultiplier = 64;

        /// <summary> the difference in rating where one person is almost certain to win </summary>
        private const double maximumSkillGap = 400;

        public IList<Player> UpdateScores(IList<Player> scoreboard, Game game)
        {
            var results = game.GetResults();
            var previousScores = new Dictionary<string, double>();

            foreach (var playerName in results.Keys)
            {
                var player = scoreboard.SingleOrDefault(p => p.Name == playerName);

                if (player == null)
                {
                    player = new Player(playerName);
                    scoreboard.Add(player);
                    player.Score = 1400;
                }

                previousScores.Add(playerName, player.Score);
            }

            foreach (var playerAName in results.Keys)
            {
                foreach (var playerBName in results.Keys)
                {
                    if (playerAName == playerBName) continue;

                    var playerAResult = results[playerAName];
                    var playerBResult = results[playerBName];

                    var playerA = scoreboard.Single(p => p.Name == playerAName);

                    var chanceOfPlayerAWinning = ChanceOfWinning(previousScores[playerAName], previousScores[playerBName]);
                    var didPlayerAWin = (playerAResult > playerBResult);
                    var ratingChange = RatingChange(chanceOfPlayerAWinning, didPlayerAWin);
                    // adjust for the fact that we're playing against multiple people
                    var adjustedRatingChange = ratingChange / results.Count;
                    var integerRatingChange = Math.Round(adjustedRatingChange, MidpointRounding.AwayFromZero);

                    playerA.AddScore(integerRatingChange);
                }
            }

            return scoreboard;
        }

        private double RatingChange(double expectedToWin, bool actuallyWon)
        {
            var w = (actuallyWon) ? 1 : 0;
            return ratingChangeBaseMultiplier*(w - expectedToWin);
        }

        /// <summary>
        /// the chance of a player with rating <param name="ratingA"/> beating a player with rating <param name="ratingB"/>
        /// </summary>
        /// <remarks>
        /// See https://www.wolframalpha.com/input/?i=plot+1%2F%281+%2B+Pow%2810%2C+%28y+-+x%29%2F400%29%29%3B
        /// for a graph of this function.
        /// </remarks>
        private double ChanceOfWinning(double ratingA, double ratingB)
        {
            return 1/(1 + Math.Pow(10.0, (ratingB - ratingA)/maximumSkillGap));
        }
    }
}