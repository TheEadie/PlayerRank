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
        public void FourPlayerGameOneRound()
        {
            var league = new League(new EloScoringStrategy());

            league.AddPlayer("David");
            league.AddPlayer("Jack");
            league.AddPlayer("Bob");
            league.AddPlayer("Chris");

            var game = new Game();

            game.AddResult("David", 100);
            game.AddResult("Jack", 50);
            game.AddResult("Bob", 25);
            game.AddResult("Chris", 0);

            league.RecordGame(game);

            Assert.Equal(1424, league.GetLeaderBoard().Where(x => x.Name == "David").Select(x => x.Score).Single());
            Assert.Equal(1408, league.GetLeaderBoard().Where(x => x.Name == "Jack").Select(x => x.Score).Single());
            Assert.Equal(1392, league.GetLeaderBoard().Where(x => x.Name == "Bob").Select(x => x.Score).Single());
            Assert.Equal(1376, league.GetLeaderBoard().Where(x => x.Name == "Chris").Select(x => x.Score).Single());
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

            // Bar won most recently therefore will be slightly ahead
            Assert.Equal(1394, league.GetLeaderBoard().Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(1406, league.GetLeaderBoard().Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }

        [Fact]
        public void FourPlayerGame40Rounds()
        {
            var league = new League(new EloScoringStrategy());

            league.AddPlayer("David");
            league.AddPlayer("Jack");
            league.AddPlayer("Bob");
            league.AddPlayer("Chris");

            for (var i = 0; i < 10; i++)
            {
                var game = new Game();

                game.AddResult("David", 100);
                game.AddResult("Jack", 50);
                game.AddResult("Bob", 25);
                game.AddResult("Chris", 0);

                league.RecordGame(game);

                var game2 = new Game();

                game2.AddResult("David", 0);
                game2.AddResult("Jack", 100);
                game2.AddResult("Bob", 50);
                game2.AddResult("Chris", 25);

                league.RecordGame(game2);

                var game3 = new Game();

                game3.AddResult("David", 25);
                game3.AddResult("Jack", 0);
                game3.AddResult("Bob", 100);
                game3.AddResult("Chris", 50);

                league.RecordGame(game3);

                var game4 = new Game();

                game4.AddResult("David", 50);
                game4.AddResult("Jack", 25);
                game4.AddResult("Bob", 0);
                game4.AddResult("Chris", 100);

                league.RecordGame(game4);
            }

            Assert.Equal(1397, league.GetLeaderBoard().Where(x => x.Name == "David").Select(x => x.Score).Single());
            Assert.Equal(1390, league.GetLeaderBoard().Where(x => x.Name == "Jack").Select(x => x.Score).Single());
            Assert.Equal(1394, league.GetLeaderBoard().Where(x => x.Name == "Bob").Select(x => x.Score).Single());
            Assert.Equal(1419, league.GetLeaderBoard().Where(x => x.Name == "Chris").Select(x => x.Score).Single());
        }
    }
}