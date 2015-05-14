using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ELORank.UnitTests
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
