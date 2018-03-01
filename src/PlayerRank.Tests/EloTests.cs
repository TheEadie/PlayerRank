using System.Linq;
using PlayerRank.Scoring.Elo;
using Xunit;

namespace PlayerRank.Tests
{
    public class EloTests
    {
        [Fact]
        public void TwoPlayerGameOneRound()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Points(100));
            game.AddResult("Bar", new Points(0));

            league.RecordGame(game);

            var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));
            var leaderboard = league.GetLeaderBoard(eloScoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(1400 + 32), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(1400 - 32), barResult.Points);
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

            var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));
            var leaderboard = league.GetLeaderBoard(eloScoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(1400 + 32), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(1400 - 32), barResult.Points);
            Assert.Equal(new Position(2), barResult.Position);
        }

        [Fact]
        public void DrawCausesNoChangeInScores()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Points(100));
            game.AddResult("Bar", new Points(100));
            game.AddResult("Bar2", new Points(100));

            league.RecordGame(game);

            var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));
            var leaderboard = league.GetLeaderBoard(eloScoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");
            var bar2Result = leaderboard.Single(x => x.Name == "Bar2");

            Assert.Equal(new Points(1400), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(1400), barResult.Points);
            Assert.Equal(new Position(1), barResult.Position);
            Assert.Equal(new Points(1400), bar2Result.Points);
            Assert.Equal(new Position(1), bar2Result.Position);
        }

        [Fact]
        public void DrawCausesNoChangeInScoresWhenRecordingByPosition()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Position(1));
            game.AddResult("Bar", new Position(1));

            league.RecordGame(game);

            var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));
            var leaderboard = league.GetLeaderBoard(eloScoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            Assert.Equal(new Points(1400), fooResult.Points);
            Assert.Equal(new Position(1), fooResult.Position);
            Assert.Equal(new Points(1400), barResult.Points);
            Assert.Equal(new Position(1), barResult.Position);
        }

        [Fact]
        public void FourPlayerGameOneRound()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("David", new Points(100));
            game.AddResult("Jack", new Points(50));
            game.AddResult("Bob", new Points(25));
            game.AddResult("Chris", new Points(0));

            league.RecordGame(game);

            var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));
            var leaderboard = league.GetLeaderBoard(eloScoringStrategy).ToList();
            var davidResult = leaderboard.Single(x => x.Name == "David");
            var jackResult = leaderboard.Single(x => x.Name == "Jack");
            var bobResult = leaderboard.Single(x => x.Name == "Bob");
            var chrisResult = leaderboard.Single(x => x.Name == "Chris");

            Assert.Equal(new Points(1433), davidResult.Points);
            Assert.Equal(new Position(1), davidResult.Position);
            Assert.Equal(new Points(1411), jackResult.Points);
            Assert.Equal(new Position(2), jackResult.Position);
            Assert.Equal(new Points(1389), bobResult.Points);
            Assert.Equal(new Position(3), bobResult.Position);
            Assert.Equal(new Points(1367), chrisResult.Points);
            Assert.Equal(new Position(4), chrisResult.Position);
        }

        [Fact]
        public void TwoPlayerGame20Rounds()
        {
            var league = new League();

            for (var i = 0; i < 10; i++)
            {
                var game = new Game();

                game.AddResult("Foo", new Points(100));
                game.AddResult("Bar", new Points(0));

                league.RecordGame(game);

                var game2 = new Game();

                game2.AddResult("Foo", new Points(0));
                game2.AddResult("Bar", new Points(100));

                league.RecordGame(game2);
            }

            var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));
            var leaderboard = league.GetLeaderBoard(eloScoringStrategy).ToList();
            var fooResult = leaderboard.Single(x => x.Name == "Foo");
            var barResult = leaderboard.Single(x => x.Name == "Bar");

            // Bar won most recently therefore will be slightly ahead
            Assert.Equal(new Points(1384), fooResult.Points);
            Assert.Equal(new Position(2), fooResult.Position);
            Assert.Equal(new Points(1416), barResult.Points);
            Assert.Equal(new Position(1), barResult.Position);
        }

        [Fact]
        public void FourPlayerGame40Rounds()
        {
            var league = new League();

            for (var i = 0; i < 10; i++)
            {
                var game = new Game();

                game.AddResult("David", new Points(100));
                game.AddResult("Jack", new Points(50));
                game.AddResult("Bob", new Points(25));
                game.AddResult("Chris", new Points(0));

                league.RecordGame(game);

                var game2 = new Game();

                game2.AddResult("David", new Points(0));
                game2.AddResult("Jack", new Points(100));
                game2.AddResult("Bob", new Points(50));
                game2.AddResult("Chris", new Points(25));

                league.RecordGame(game2);

                var game3 = new Game();

                game3.AddResult("David", new Points(25));
                game3.AddResult("Jack", new Points(0));
                game3.AddResult("Bob", new Points(100));
                game3.AddResult("Chris", new Points(50));

                league.RecordGame(game3);

                var game4 = new Game();

                game4.AddResult("David", new Points(50));
                game4.AddResult("Jack", new Points(25));
                game4.AddResult("Bob", new Points(0));
                game4.AddResult("Chris", new Points(100));

                league.RecordGame(game4);
            }

            var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));
            var leaderboard = league.GetLeaderBoard(eloScoringStrategy).ToList();
            var davidResult = leaderboard.Single(x => x.Name == "David");
            var jackResult = leaderboard.Single(x => x.Name == "Jack");
            var bobResult = leaderboard.Single(x => x.Name == "Bob");
            var chrisResult = leaderboard.Single(x => x.Name == "Chris");

            Assert.Equal(new Points(1396), davidResult.Points);
            Assert.Equal(new Position(2), davidResult.Position);
            Assert.Equal(new Points(1383), jackResult.Points);
            Assert.Equal(new Position(4), jackResult.Position);
            Assert.Equal(new Points(1394), bobResult.Points);
            Assert.Equal(new Position(3), bobResult.Position);
            Assert.Equal(new Points(1427), chrisResult.Points);
            Assert.Equal(new Position(1), chrisResult.Position);
        }
    }
}