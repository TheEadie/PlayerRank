using System.Linq;
using Xunit;

namespace ELORank.UnitTests
{
    public class EloTests
    {
        [Fact]
        public void TwoPlayerGameOneRound()
        {
            var league = new League(new EloScoringStrategy());

            league.AddPlayer("Foo");
            league.AddPlayer("Bar");

            var game = new Game();

            game.AddResult("Foo", 100);
            game.AddResult("Bar", 0);

            league.RecordGame(game);

            Assert.Equal(1400 + 16, league.GetLeaderBoard().Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(1400 - 16, league.GetLeaderBoard().Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }

    }
}