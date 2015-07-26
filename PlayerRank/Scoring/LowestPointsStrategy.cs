using System;
using System.Collections.Generic;

namespace PlayerRank.Scoring
{
    internal class LowestPointsStrategy : IScoringStrategy
    {
        public LowestPointsStrategy()
        {
        }

        public IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game)
        {
            throw new NotImplementedException();
        }
    }
}