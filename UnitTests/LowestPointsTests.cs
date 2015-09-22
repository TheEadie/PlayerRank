using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PlayerRank.Scoring;
using PlayerRank.Scoring.LowestPoints;
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

            game.AddResult("Foo", new Points(1));
            game.AddResult("Bar", new Points(2));

            league.RecordGame(game);

            var leaderboard = league.GetLeaderBoard(new LowestPointsStrategy()).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(1), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(2), barResult.Points);
            Assert.Equal(new Position(2), barResult.Position);
        }

        [Fact]
        public void TwoPlayerGameOneRoundByPosition()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Position(1));
            game.AddResult("Bar", new Position(2));

            league.RecordGame(game);

            var leaderboard = league.GetLeaderBoard(new LowestPointsStrategy()).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(1), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(2), barResult.Points);
            Assert.Equal(new Position(2), barResult.Position);
        }

        [Fact]
        public void TwoPlayerGameFourRoundsWithOneDiscard()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Points(1));
            game.AddResult("Bar", new Points(2));

            var game2 = new Game();

            game2.AddResult("Foo", new Points(1));
            game2.AddResult("Bar", new Points(5));

            var game3 = new Game();

            game3.AddResult("Foo", new Points(2));
            game3.AddResult("Bar", new Points(1));

            var game4 = new Game();

            game4.AddResult("Foo", new Points(3));
            game4.AddResult("Bar", new Points(1));

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);
            league.RecordGame(game4);

            var discard = new DiscardPolicy(1, 0);
            var scoringStrategy = new LowestPointsStrategy(discard);

            var leaderboard = league.GetLeaderBoard(scoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(4), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(4), barResult.Points);
            Assert.Equal(new Position(1), barResult.Position);
        }

        [Fact]
        public void TwoPlayerGameFourRoundsWithTwoDiscards()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Points(1));
            game.AddResult("Bar", new Points(2));

            var game2 = new Game();

            game2.AddResult("Foo", new Points(2));
            game2.AddResult("Bar", new Points(5));

            var game3 = new Game();

            game3.AddResult("Foo", new Points(4));
            game3.AddResult("Bar", new Points(1));

            var game4 = new Game();

            game4.AddResult("Foo", new Points(3));
            game4.AddResult("Bar", new Points(1));

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);
            league.RecordGame(game4);

            var discard = new DiscardPolicy(2, 0);
            var scoringStrategy = new LowestPointsStrategy(discard);

            var leaderboard = league.GetLeaderBoard(scoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(3), fooResult.Points);
            Assert.Equal(new Position(2), fooResult.Position);
            Assert.Equal(new Points(2), barResult.Points);
            Assert.Equal(new Position(1), barResult.Position);
        }

        [Fact]
        public void OnlyDiscardIfPlayedEnoughGames()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Points(1));
            game.AddResult("Bar", new Points(2));

            var game2 = new Game();

            game2.AddResult("Foo", new Points(2));
            game2.AddResult("Bar", new Points(5));

            var game3 = new Game();

            game3.AddResult("Foo", new Points(4));
            game3.AddResult("Bar", new Points(1));

            var game4 = new Game();

            game4.AddResult("Foo", new Points(3));
            game4.AddResult("Bar2", new Points(1));

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);
            league.RecordGame(game4);

            var discard = new DiscardPolicy(1, 4);
            var scoringStrategy = new LowestPointsStrategy(discard);

            var leaderboard = league.GetLeaderBoard(scoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(6), fooResult.Points);
            Assert.Equal(new Position(2), fooResult.Position);
            Assert.Equal(new Points(8), barResult.Points);
            Assert.Equal(new Position(3), barResult.Position);
        }

        [Fact]
        public void MultipleDiscardPolicies()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Points(1));
            game.AddResult("Bar", new Points(2));

            var game2 = new Game();

            game2.AddResult("Foo", new Points(2));
            game2.AddResult("Bar", new Points(5));

            var game3 = new Game();

            game3.AddResult("Foo", new Points(4));
            game3.AddResult("Bar", new Points(1));

            var game4 = new Game();

            game4.AddResult("Foo", new Points(3));
            game4.AddResult("Bar2", new Points(1));

            league.RecordGame(game);
            league.RecordGame(game2);
            league.RecordGame(game3);
            league.RecordGame(game4);

            var discard = new DiscardPolicy(1, 2);
            var discard2 = new DiscardPolicy(2, 4);
            var scoringStrategy = new LowestPointsStrategy(discard, discard2);

            var leaderboard = league.GetLeaderBoard(scoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(3), fooResult.Points);
            Assert.Equal(new Position(2), fooResult.Position);
            Assert.Equal(new Points(3), barResult.Points);
            Assert.Equal(new Position(2), barResult.Position);
        }
    }
}