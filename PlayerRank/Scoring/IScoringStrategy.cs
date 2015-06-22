using System.Collections.Generic;

namespace PlayerRank.Scoring
{
    public interface IScoringStrategy
    {
        IList<Player> UpdateScores(IList<Player> scoreboard, Game game);
    }
}