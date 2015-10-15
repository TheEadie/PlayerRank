using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Stats
{
    public static class ResultChangeStats
    {
        public static IEnumerable<ResultsChange> GetResultChangesBewteenGames(IList<History> history, int gameA, int gameB)
        {
            var changes = new List<ResultsChange>();

            var players = history[gameA - 1].Game.GetResults().Select(x => x.Name);

            var leaderboardA = history[gameA - 1].Leaderboard;
            var leaderboardB = history[gameB - 1].Leaderboard;

            foreach (var player in players)
            {
                var resultA = leaderboardA.Single(x => x.Name == player);
                var resultB = leaderboardB.SingleOrDefault(x => x.Name == player);

                int positionChange;
                double pointsChange;

                if (resultB == null)
                {
                    positionChange = 0;
                    pointsChange = 0;
                }
                else
                {
                    positionChange = -(resultA.Position.GetValue() - resultB.Position.GetValue());
                    pointsChange = (resultA.Points - resultB.Points).GetValue();
                }
                

                changes.Add(new ResultsChange(player, resultA.Position, positionChange, resultA.Points, pointsChange));
            }

            return changes;
        }
    }
}