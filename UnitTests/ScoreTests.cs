using Xunit;

namespace PlayerRank.UnitTests
{
    public class ScoreTests
    {
        [Fact]
        public void ScoresAreEqual()
        {
            var scoreA = new Score(100);
            var scoreb = new Score(100);

            Assert.Equal(scoreA, scoreb);
        }
    }
}