﻿using System.Linq;
using PlayerRank.Scoring.Simple;
using PlayerRank.Stats;
using Xunit;

namespace PlayerRank.UnitTests
{
    public class ResultChangeStatsTests
    {
        [Fact]
        public void CanGetDifferenceBetweenTwoGames()
        {
            var league = new League();

            for (var i = 0; i < 10; i++)
            {
                var game = new Game();
                game.AddResult("Foo", new Points(5));
                game.AddResult("Bar", new Points(1));

                league.RecordGame(game);
            }

            var history = league.GetLeaderBoardHistory(new SimpleScoringStrategy()).ToList();

            var change = ResultChangeStats.GetResultChangesBewteenGames(history, history.Count, history.Count - 1);

            var resultsChangeForFoo = change.Single(x => x.Name == "Foo");

            Assert.Equal(0, resultsChangeForFoo.PositionChange);
            Assert.Equal(5, resultsChangeForFoo.PointsChange);
        }
    }
}