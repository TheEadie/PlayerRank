using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void PositionsCanBeOrdered()
        {
            var positionFirst = new Position(1);
            var positionSecond = new Position(2);
            var positionThird = new Position(3);

            var positions = new List<Position> {positionSecond, positionFirst, positionThird};
            var positionsOrdered = new List<Position> { positionFirst, positionSecond, positionThird };

            var positionsFromTest = positions.OrderBy(x => x);

            Assert.Equal(positionsOrdered, positionsFromTest);
        }
    }
}