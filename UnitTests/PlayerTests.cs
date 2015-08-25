using Xunit;

namespace PlayerRank.UnitTests
{
    public class PlayerTests
    {
        [Fact]
        public void CanIncreasePlayersScore()
        {
            var player = new PlayerScore("Foo");
            player.AddPoints(new Points(100));

            Assert.Equal(new Points(100), player.Points);
            Assert.Equal(100, player.Score);
        }

        [Fact]
        public void CanReducePlayersScore()
        {
            var player = new PlayerScore("Foo");
            player.SubtractPoints(new Points(100));

            Assert.Equal(new Points(-100), player.Points);
            Assert.Equal(-100, player.Score);
        }
    }
}
