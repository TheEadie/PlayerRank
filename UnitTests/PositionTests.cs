using Xunit;

namespace PlayerRank.UnitTests
{
    public class PositionTests
    {
        [Fact]
        public void PositionsAreEqual()
        {
            var positionA = new Position(1);
            var positionB = new Position(1);

            Assert.Equal(positionA, positionB);
        }
    }
}