using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring
{
    internal class LowestPointsStrategy : IScoringStrategy
    {
        private readonly IList<Game> m_allResults = new List<Game>();
        private readonly IList<Discard> m_Discards;

        public LowestPointsStrategy(params Discard[] discards)
        {
            m_Discards = discards.Any()
                ? discards.OrderBy(x => x.GamesToBePlayed).ToList()
                : new List<Discard>();
        }

        public void Reset()
        {
            m_allResults.Clear();
        }

        public IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game)
        {
            var allResultsPrev = m_allResults.SelectMany(x => x.GetResults()).ToList();

            m_allResults.Add(game);

            var allResultsNow = m_allResults.SelectMany(x => x.GetResults());

            foreach (var result in game.GetResults())
            {
                var player = scoreboard.SingleOrDefault(p => p.Name == result.Key);

                if (player == null)
                {
                    player = new PlayerScore(result.Key);
                    scoreboard.Add(player);
                    player.Score = 0;
                }

                player.AddScore(result.Value);

                // Add back previous worst results
                FindWorstResults(allResultsPrev, player, false);

                // Subtract worst results
                FindWorstResults(allResultsNow, player, true);
            }

            return scoreboard;
        }

        private void FindWorstResults(IEnumerable<KeyValuePair<string, double>> allResultsNow, PlayerScore player,
            bool subtract)
        {
            var allResultsForPlayer =
                allResultsNow.Where(x => x.Key == player.Name)
                    .Select(x => x.Value)
                    .OrderByDescending(x => x)
                    .ToList();

            var discardPolicy = new Discard(0, 0);

            // Find the relavent policy
            foreach (var dp in m_Discards)
            {
                if (allResultsForPlayer.Count >= dp.GamesToBePlayed)
                {
                    discardPolicy = dp;
                }
            }

            for (var i = 0; i < discardPolicy.NumberOfdiscards; i++)
            {
                if (allResultsForPlayer.Count > i)
                {
                    var nextWorstScore = subtract ? -allResultsForPlayer[i] : allResultsForPlayer[i];
                    player.AddScore(nextWorstScore);
                }
            }
        }
    }
}