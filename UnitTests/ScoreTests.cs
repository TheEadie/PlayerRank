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

        [Fact]
        public void CanAddScores()
        {
            var scoreA = new Score(100);
            var scoreb = new Score(100);

            var expected = new Score(200);
            var actual = scoreA + scoreb;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanSubtractScores()
        {
            var scoreA = new Score(100);
            var scoreb = new Score(100);

            var expected = new Score(0);
            var actual = scoreA - scoreb;

            Assert.Equal(expected, actual);
        }
    }
}