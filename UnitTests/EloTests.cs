using System.Linq;
using PlayerRank.Scoring;
using PlayerRank.Scoring.Elo;
using Xunit;

namespace PlayerRank.UnitTests
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
            Assert.Equal(new Points(1400 + 16), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Foo").Select(x => x.Points).Single());
            Assert.Equal(new Points(1400 - 16), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Bar").Select(x => x.Points).Single());
        }

        [Fact]
        public void DrawCausesNoChangeInScores()
        {
            var league = new League();

            var game = new Game();

            game.AddResult("Foo", new Points(100));
            game.AddResult("Bar", new Points(100));

            league.RecordGame(game);

            var eloScoringStrategy = new EloScoringStrategy(new Points(64), new Points(400), new Points(1400));
            Assert.Equal(new Points(1400), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Foo").Select(x => x.Points).Single());
            Assert.Equal(new Points(1400), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Bar").Select(x => x.Points).Single());
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
            Assert.Equal(new Points(1424), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "David").Select(x => x.Points).Single());
            Assert.Equal(new Points(1408), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Jack").Select(x => x.Points).Single());
            Assert.Equal(new Points(1392), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Bob").Select(x => x.Points).Single());
            Assert.Equal(new Points(1376), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Chris").Select(x => x.Points).Single());
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

            // Bar won most recently therefore will be slightly ahead
            Assert.Equal(new Points(1394), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Foo").Select(x => x.Points).Single());
            Assert.Equal(new Points(1406), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Bar").Select(x => x.Points).Single());
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

            Assert.Equal(new Points(1397), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "David").Select(x => x.Points).Single());
            Assert.Equal(new Points(1390), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Jack").Select(x => x.Points).Single());
            Assert.Equal(new Points(1394), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Bob").Select(x => x.Points).Single());
            Assert.Equal(new Points(1419), league.GetLeaderBoard(eloScoringStrategy).Where(x => x.Name == "Chris").Select(x => x.Points).Single());
        }
    }
}