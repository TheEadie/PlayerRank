using System.Linq;
using PlayerRank.Scoring;
using Xunit;

namespace PlayerRank.UnitTests
{
    public class LowestPointsTests
    {
        [Fact]
        public void TwoPlayerGameOneRound()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", 1);
            game.AddResult("Bar", 2);

            league.RecordGame(game);

            var scoringStrategy = new LowestPointsStrategy();
            Assert.Equal(1, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(2, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }
    }
}