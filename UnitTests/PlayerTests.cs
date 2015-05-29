using Xunit;

namespace PlayerRank.UnitTests
{
    public class PlayerTests
    {
        [Fact]
        public void CanIncreasePlayersScore()
        {
            var player = new Player("Foo");
            player.AddScore(100);
        }

        [Fact]
        public void CanReducePlayersScore()
        {
            var player = new Player("Foo");
            player.AddScore(-100);
        }
    }
}
