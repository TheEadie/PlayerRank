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
            Assert.Equal(100, player.Score);
        }

        [Fact]
        public void CanReducePlayersScore()
        {
            var player = new PlayerScore("Foo");
            player.AddScore(new Score(-100));

            Assert.Equal(new Score(-100), player.Rating);
            Assert.Equal(-100, player.Score);
        }

        [Fact]
        public void CanIncreasePlayersScoreOld()
        {
            var player = new PlayerScore("Foo");
            player.AddScore(100);

            Assert.Equal(new Score(100), player.Rating);
            Assert.Equal(100, player.Score);
        }

        [Fact]
        public void CanReducePlayersScoreOld()
        {
            var player = new PlayerScore("Foo");
            player.AddScore(-100);

            Assert.Equal(new Score(-100), player.Rating);
            Assert.Equal(-100, player.Score);
        }
    }
}
