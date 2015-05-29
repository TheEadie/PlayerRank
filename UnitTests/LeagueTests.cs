using System.Linq;
using Xunit;

namespace PlayerRank.UnitTests
{
    public class LeagueTests
    {
        [Fact]
        public void CanAddPlayerToLeague()
        {
            var league = new League(new SimpleScoringStrategy());
            league.AddPlayer("Foo");

            Assert.Contains("Foo", league.GetLeaderBoard().Select(x => x.Name));
        }

        [Fact]
        public void CanRecordSimpleGame()
        {
            var league = new League(new SimpleScoringStrategy());
            league.AddPlayer("Foo");
            league.AddPlayer("Bar");

            var game = new Game();
            game.AddResult("Foo", 5);
            game.AddResult("Bar", 1);

            league.RecordGame(game);

            Assert.Equal(5.0, league.GetLeaderBoard().Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(1.0, league.GetLeaderBoard().Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }

        [Fact]
        public void CanRecordMultipleSimpleGame()
        {
            var league = new League(new SimpleScoringStrategy());
            league.AddPlayer("Foo");
            league.AddPlayer("Bar");

            var game = new Game();
            game.AddResult("Foo", 5);
            game.AddResult("Bar", 1);

            var game2 = new Game();
            game2.AddResult("Foo", 3);
            game2.AddResult("Bar", 2);

            league.RecordGame(game);
            league.RecordGame(game2);

            Assert.Equal(8.0, league.GetLeaderBoard().Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(3.0, league.GetLeaderBoard().Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }
    }
}