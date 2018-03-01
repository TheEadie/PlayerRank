using System.Collections.Generic;

namespace PlayerRank
{
    public class History
    {
        public Game Game { get; }
        public IEnumerable<PlayerScore> Leaderboard { get; }

        public History(Game game, IEnumerable<PlayerScore> leaderboard)
        {
            Game = game;
            Leaderboard = leaderboard;
        }
    }
}