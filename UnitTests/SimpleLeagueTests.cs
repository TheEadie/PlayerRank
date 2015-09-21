using System.Collections.Generic;
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

            var leaderboard = league.GetLeaderBoard(new SimpleScoringStrategy()).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(5), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(1), barResult.Points);
            Assert.Equal(new Position(2), barResult.Position);
        }

        [Fact]
        public void CanRecordSimpleGameByPositions()
        {
            var league = new League();

            var game = new Game();
            game.AddResult("Foo", new Position(2));
            game.AddResult("Bar", new Position(1));

            league.RecordGame(game);

            var leaderboard = league.GetLeaderBoard(new SimpleScoringStrategy()).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Position(2), fooResult.Position);
            Assert.Equal(new Position(1), barResult.Position);
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

            var leaderboard = league.GetLeaderBoard(new SimpleScoringStrategy()).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(8), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(3), barResult.Points);
            Assert.Equal(new Position(2), barResult.Position);
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

            var leaderboard = league.GetLeaderBoard(new SimpleScoringStrategy()).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Position(2), barResult.Position);
        }
    }
}