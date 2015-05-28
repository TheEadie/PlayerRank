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

        [Fact]
        public void TwoPlayerGame20Rounds()
        {
            var league = new League(new EloScoringStrategy());

            league.AddPlayer("Foo");
            league.AddPlayer("Bar");

            for (var i = 0; i < 10; i++)
            {
                var game = new Game();

                game.AddResult("Foo", 100);
                game.AddResult("Bar", 0);

                league.RecordGame(game);

                var game2 = new Game();

                game2.AddResult("Foo", 0);
                game2.AddResult("Bar", 100);

                league.RecordGame(game2);
            }
            
            // Foo won most recently therefore will be slightly ahead
            Assert.Equal(1394, league.GetLeaderBoard().Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(1406, league.GetLeaderBoard().Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }

    }
}