﻿using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring
{
    internal class LowestPointsStrategy : IScoringStrategy
    {
        private int m_discards;

        public LowestPointsStrategy(int discards = 0)
        {
            m_discards = discards;
        }

        public IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game)
        {
            foreach (var result in game.GetResults())
            {
                var player = scoreboard.SingleOrDefault(p => p.Name == result.Key);

                if (player == null)
                {
                    player = new PlayerScore(result.Key);
                    scoreboard.Add(player);
                    player.Score = 0;
                }

                player.AddScore(result.Value);
            }

            return scoreboard;
        }
    }
}