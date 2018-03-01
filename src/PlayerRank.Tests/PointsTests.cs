using Xunit;

namespace PlayerRank.Tests
{
    public class PointsTests
    {
        [Fact]
        public void PointsAreEqual()
        {
            var pointsA = new Points(100);
            var pointsB = new Points(100);

            Assert.Equal(pointsA, pointsB);
        }

        [Fact]
        public void CanAddPoints()
        {
            var pointsA = new Points(100);
            var pointsB = new Points(100);

            var expected = new Points(200);
            var actual = pointsA + pointsB;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanSubtractPoints()
        {
            var pointsA = new Points(100);
            var pointsB = new Points(100);

            var expected = new Points(0);
            var actual = pointsA - pointsB;

            Assert.Equal(expected, actual);
        }
    }
}