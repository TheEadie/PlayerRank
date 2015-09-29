using System.Collections.Generic;

namespace PlayerRank.Scoring
{
    public interface IScoringStrategy
    {
        /// <summary>
        /// Updates the provided scoreboard with the results of a <see cref="Game"/> 
        /// </summary>
        IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game);

        /// <summary>
        /// Resets any state that may be stored in the strategy
        /// This should be called before calculating a fresh leaderboard
        /// </summary>
        void Reset();

        /// <summary>
        /// Sets the <see cref="Position"/> of each player based on the strategy
        /// </summary>
        /// <param name="leaderBoard"></param>
        void SetPositions(IList<PlayerScore> leaderBoard);
    }
}