using System.Linq;
using PlayerRank.Scoring;
using PlayerRank.Scoring.Simple;
using Xunit;

namespace PlayerRank.UnitTests
{
    public class LeagueTests
    {
        [Fact]
        public void CanRecordSimpleGame()
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
        public void CanRecordMultipleSimpleGame()
        {
            var league = new League();

            var game = new Game();
            game.AddResult("Foo", 5);
            game.AddResult("Bar", 1);

            var game2 = new Game();
            game2.AddResult("Foo", 3);
            game2.AddResult("Bar", 2);

            league.RecordGame(game);
            league.RecordGame(game2);

            Assert.Equal(8.0, league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(3.0, league.GetLeaderBoard(new SimpleScoringStrategy()).Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }
    }
}