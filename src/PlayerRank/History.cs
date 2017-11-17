using System.Collections.Generic;

namespace PlayerRank
{
    public class History
    {
        public Game Game { get; private set; }
        public IEnumerable<PlayerScore> Leaderboard { get; private set; }

        public History(Game game, IEnumerable<PlayerScore> leaderboard)
        {
            Game = game;
            Leaderboard = leaderboard;
        }
    }
}