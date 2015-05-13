using System.Linq;
using Xunit;

namespace ELORank.UnitTests
{
    public class LeagueTests
    {
        [Fact]
        public void CanAddPlayerToLeague()
        {
            var league = new League();
            league.AddPlayer("Foo");

            Assert.Contains("Foo", league.GetLeaderBoard().Select(x => x.Name));
        }

        [Fact]
        public void CanRecordSimpleResult()
        {
            var league = new League();
            league.AddPlayer("Foo");
            league.AddPlayer("Bar");

            var result = new Result();
            result.Scores.Add("Foo", 5);
            result.Scores.Add("Bar", 1);

            league.RecordResult(result);

            Assert.Equal(5.0, league.GetLeaderBoard().Where(x => x.Name == "Foo").Select(x => x.Score).Single());
            Assert.Equal(1.0, league.GetLeaderBoard().Where(x => x.Name == "Bar").Select(x => x.Score).Single());
        }
    }
}