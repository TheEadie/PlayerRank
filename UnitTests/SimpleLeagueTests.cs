using System.Linq;
using PlayerRank.Scoring;
using PlayerRank.Scoring.Simple;
using Xunit;

namespace PlayerRank.UnitTests
{
    public class SimpleLeagueTests
    {
        [Fact]
        public void CanRecordSimpleGameByScores()
        {
            var league = new League();

            var game = new Game();
            game.AddResult("Foo", new Points(5));
            game.AddResult("Bar", new Points(1));

            league.RecordGame(game);

            Assert.Equal(new Points(5), league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Foo").Select(x => x.Points).Single());
            Assert.Equal(new Points(1), league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Bar").Select(x => x.Points).Single());
        }

        [Fact]
        public void CanRecordSimpleGameByPositions()
        {
            var league = new League();

            var game = new Game();
            game.AddResult("Foo", new Position(2));
            game.AddResult("Bar", new Position(1));

            league.RecordGame(game);

            Assert.Equal(new Position(2), league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Foo").Select(x => x.Position).Single());
            Assert.Equal(new Position(1), league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Bar").Select(x => x.Position).Single());
        }

        [Fact]
        public void CanRecordMultipleSimpleGames()
        {
            var league = new League();

            var game = new Game();
            game.AddResult("Foo", new Points(5));
            game.AddResult("Bar", new Points(1));

            var game2 = new Game();
            game2.AddResult("Foo", new Points(3));
            game2.AddResult("Bar", new Points(2));

            league.RecordGame(game);
            league.RecordGame(game2);

            Assert.Equal(new Points(8), league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Foo").Select(x => x.Points).Single());
            Assert.Equal(new Points(3), league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Bar").Select(x => x.Points).Single());
        }

        [Fact]
        public void CanRecordMultipleSimpleGamesByPositions()
        {
            var league = new League();

            var game = new Game();
            game.AddResult("Foo", new Position(2));
            game.AddResult("Bar", new Position(1));

            var game2 = new Game();
            game2.AddResult("Foo", new Position(1));
            game2.AddResult("Bar", new Position(2));

            var game3 = new Game();
            game3.AddResult("Foo", new Position(1));
            game3.AddResult("Bar", new Position(2));

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);

            Assert.Equal(new Position(1), league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Foo").Select(x => x.Position).Single());
            Assert.Equal(new Position(2), league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Bar").Select(x => x.Position).Single());
        }
    }
}