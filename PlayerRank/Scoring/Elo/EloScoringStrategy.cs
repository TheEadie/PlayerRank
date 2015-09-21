﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring.Elo
{
    public class EloScoringStrategy : IScoringStrategy
    {
        /// <summary> also known as K in the ELO formula - the max change in rating from one game </summary>
        private readonly Points m_RatingChangeBaseMultiplier;

        /// <summary> the difference in rating where one person is almost certain to win </summary>
        private readonly Points m_MaximumSkillGap;

        private readonly Points m_NewPlayerStartingRating;

        public EloScoringStrategy(Points maxRatingChange, Points maxSkillGap, Points startingRating)
        {
            m_RatingChangeBaseMultiplier = maxRatingChange;
            m_MaximumSkillGap = maxSkillGap;
            m_NewPlayerStartingRating = startingRating;
        }

        [Obsolete("This cconstructor will be removed in a later version. " +
                  "Please use EloScoringStrategy(Points, Points, Points) instead")]
        public EloScoringStrategy(double maxRatingChange, double maxSkillGap, double startingRating)
        {
            m_RatingChangeBaseMultiplier = new Points(maxRatingChange);
            m_MaximumSkillGap = new Points(maxSkillGap);
            m_NewPlayerStartingRating = new Points(startingRating);
        }

        public void Reset()
        {
        }

        public IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game)
        {
            var results = game.GetResults();
            var previousScores = new Dictionary<string, Points>();

            foreach (var playerName in results.Select(x => x.Name))
            {
                var player = scoreboard.SingleOrDefault(p => p.Name == playerName);

                if (player == null)
                {
                    player = new PlayerScore(playerName);
                    scoreboard.Add(player);
                    player.Points = m_NewPlayerStartingRating;
                }

                previousScores.Add(playerName, player.Points);
            }

            foreach (var playerAName in results.Select(x => x.Name))
            {
                foreach (var playerBName in results.Select(x => x.Name))
                {
                    if (playerAName == playerBName) continue;

                    var playerAResult = results.Single(x => x.Name == playerAName).Points;
                    var playerBResult = results.Single(x => x.Name == playerBName).Points;

                    var playerA = scoreboard.Single(p => p.Name == playerAName);

                    var chanceOfPlayerAWinning = ChanceOfWinning(previousScores[playerAName], previousScores[playerBName]);

                    // If the players have drawn then don't update their scores
                    if (playerAResult == playerBResult)
                    {
                        continue;
                    }

                    var didPlayerAWin = (playerAResult > playerBResult);
                    var ratingChange = RatingChange(chanceOfPlayerAWinning, didPlayerAWin);
                    // adjust for the fact that we're playing against multiple people
                    var adjustedRatingChange = ratingChange / results.Count;
                    var integerRatingChange = Math.Round(adjustedRatingChange, MidpointRounding.AwayFromZero);

                    playerA.AddPoints(new Points(integerRatingChange));
                }
            }

            scoreboard = scoreboard.OrderByDescending(p => p.Points).ToList();

            for (var i = 0; i < scoreboard.Count; i++)
            {
                scoreboard[i].Position = new Position(i + 1);
            }

            return scoreboard;
        }

        private Points RatingChange(double expectedToWin, bool actuallyWon)
        {
            var w = (actuallyWon) ? 1 : 0;
            return m_RatingChangeBaseMultiplier * new Points(w - expectedToWin);
        }

        /// <summary>
        /// the chance of a player with rating <param name="ratingA"/> beating a player with rating <param name="ratingB"/>
        /// </summary>
        /// <remarks>
        /// See https://www.wolframalpha.com/input/?i=plot+1%2F%281+%2B+Pow%2810%2C+%28y+-+x%29%2F400%29%29%3B
        /// for a graph of this function.
        /// </remarks>
        private double ChanceOfWinning(Points ratingA, Points ratingB)
        {
            return 1 /(1 + Points.Pow(10.0, ((ratingB - ratingA) / m_MaximumSkillGap)));
        }
    }
}