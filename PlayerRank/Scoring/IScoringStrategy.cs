using System.Collections.Generic;

namespace PlayerRank.Scoring
{
    public interface IScoringStrategy
    {
        IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game);
        void Reset();
        void SetPositions(IList<PlayerScore> leaderBoard);
    }
}