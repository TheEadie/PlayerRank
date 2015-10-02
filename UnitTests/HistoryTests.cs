using System.Linq;
using PlayerRank.Scoring.Simple;
using Xunit;

namespace PlayerRank.UnitTests
{
    public class HistoryTests
    {
        [Fact]
        public void CanGetHistoryOfLeague()
        {
            var league = new League();

            for (var i = 0; i < 10; i++)
            {
                var game = new Game();
                game.AddResult("Foo", new Points(5));
                game.AddResult("Bar", new Points(1));

                league.RecordGame(game);
            }

            var history = league.GetLeaderBoardHistory(new SimpleScoringStrategy()).ToList();
            var fooAfterFiveGames = history[4].Leaderboard.Single(x => x.Name == "Foo");
            var fooMostRecentGame = history.Last().Leaderboard.Single(x => x.Name == "Foo");

            Assert.Equal(new Points(25), fooAfterFiveGames.Points);
            Assert.Equal(new Points(50), fooMostRecentGame.Points);
        }
    }
}