using System.Linq;
using System.Runtime.CompilerServices;
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

        [Fact]
        public void TwoPlayerGameFourRoundsWithOneDiscard()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", 1);
            game.AddResult("Bar", 2);

            var game2 = new Game();

            game2.AddResult("Foo", 1);
            game2.AddResult("Bar", 5);

            var game3 = new Game();

            game3.AddResult("Foo", 2);
            game3.AddResult("Bar", 1);

            var game4 = new Game();

            game4.AddResult("Foo", 3);
            game4.AddResult("Bar", 1);

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);
            league.RecordGame(game4);

            var discard = new Discard(1, 0);

            var scoringStrategy = new LowestPointsStrategy(discard);
            Assert.Equal(4, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(4, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }

        [Fact]
        public void TwoPlayerGameFourRoundsWithTwoDiscards()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", 1);
            game.AddResult("Bar", 2);

            var game2 = new Game();

            game2.AddResult("Foo", 2);
            game2.AddResult("Bar", 5);

            var game3 = new Game();

            game3.AddResult("Foo", 4);
            game3.AddResult("Bar", 1);

            var game4 = new Game();

            game4.AddResult("Foo", 3);
            game4.AddResult("Bar", 1);

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);
            league.RecordGame(game4);

            var discard = new Discard(2, 0);

            var scoringStrategy = new LowestPointsStrategy(discard);
            Assert.Equal(3, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(2, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }

        [Fact]
        public void OnlyDiscardIfPlayedEnoughGames()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", 1);
            game.AddResult("Bar", 2);

            var game2 = new Game();

            game2.AddResult("Foo", 2);
            game2.AddResult("Bar", 5);

            var game3 = new Game();

            game3.AddResult("Foo", 4);
            game3.AddResult("Bar", 1);

            var game4 = new Game();

            game4.AddResult("Foo", 3);
            game4.AddResult("Bar2", 1);

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);
            league.RecordGame(game4);

            var discard = new Discard(1, 4);

            var scoringStrategy = new LowestPointsStrategy(discard);
            Assert.Equal(6, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(8, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }

        [Fact]
        public void MultipleDiscardPolicies()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", 1);
            game.AddResult("Bar", 2);

            var game2 = new Game();

            game2.AddResult("Foo", 2);
            game2.AddResult("Bar", 5);

            var game3 = new Game();

            game3.AddResult("Foo", 4);
            game3.AddResult("Bar", 1);

            var game4 = new Game();

            game4.AddResult("Foo", 3);
            game4.AddResult("Bar2", 1);

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);
            league.RecordGame(game4);

            var discard = new Discard(1, 2);
            var discard2 = new Discard(2, 4);

            var scoringStrategy = new LowestPointsStrategy(discard, discard2);
            Assert.Equal(6, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(8, league.GetLeaderBoard(scoringStrategy).Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }
    }
}