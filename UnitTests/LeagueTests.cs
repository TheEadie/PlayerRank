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
    }
}