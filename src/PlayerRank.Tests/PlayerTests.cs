using Xunit;

namespace PlayerRank.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void CanIncreasePlayersScore()
        {
            var player = new PlayerScore("Foo");
            player.AddPoints(new Points(100));

            Assert.Equal(new Points(100), player.Points);
        }

        [Fact]
        public void CanReducePlayersScore()
        {
            var player = new PlayerScore("Foo");
            player.SubtractPoints(new Points(100));

            Assert.Equal(new Points(-100), player.Points);
        }
    }
}
