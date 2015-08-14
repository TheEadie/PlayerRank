using Xunit;

namespace PlayerRank.UnitTests
{
    public class PlayerTests
    {
        [Fact]
        public void CanIncreasePlayersScore()
        {
            var player = new PlayerScore("Foo");
            player.AddScore(new Score(100));

            Assert.Equal(new Score(100), player.Rating);
        }

        [Fact]
        public void CanReducePlayersScore()
        {
            var player = new PlayerScore("Foo");
            player.AddScore(new Score(-100));

            Assert.Equal(new Score(-100), player.Rating);
        }
    }
}
