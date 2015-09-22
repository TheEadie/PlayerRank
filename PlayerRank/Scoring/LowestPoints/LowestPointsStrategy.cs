using System.Collections.Generic;
using System.Linq;

namespace PlayerRank.Scoring.LowestPoints
{
    public class LowestPointsStrategy : IScoringStrategy
    {
        private readonly IList<Game> m_allResults = new List<Game>();
        private readonly IList<DiscardPolicy> m_Discards;

        public LowestPointsStrategy(params DiscardPolicy[] discards)
        {
            m_Discards = discards.Any()
                ? discards.OrderBy(x => x.GamesToBePlayed).ToList()
                : new List<DiscardPolicy>();
        }

        public void Reset()
        {
            m_allResults.Clear();
        }

        public void SetPositions(IList<PlayerScore> leaderBoard)
        {
            leaderBoard = leaderBoard.OrderBy(p => p.Points).ToList();

            for (var i = 0; i < leaderBoard.Count; i++)
            {
                var position = (i > 0 && leaderBoard[i].Points == leaderBoard[i - 1].Points)
                    ? new Position(i)
                    : new Position(i + 1);

                leaderBoard[i].Position = position;
            }
        }

        public IList<PlayerScore> UpdateScores(IList<PlayerScore> scoreboard, Game game)
        {
            var allResultsPrev = m_allResults.SelectMany(x => x.GetResults()).ToList();

            m_allResults.Add(game);

            var allResultsNow = m_allResults.SelectMany(x => x.GetResults()).ToList();

            foreach (var result in game.GetResults())
            {
                var player = scoreboard.SingleOrDefault(p => p.Name == result.Name);

                if (player == null)
                {
                    player = new PlayerScore(result.Name);
                    scoreboard.Add(player);
                    player.Points = new Points(0);
                }

                player.AddPoints(result.Points);

                // Add back previous worst results
                player.AddPoints(SumWorstResults(allResultsPrev, player));

                // Subtract worst results
                player.SubtractPoints(SumWorstResults(allResultsNow, player));
            }

            return scoreboard;
        }

        private Points SumWorstResults(IEnumerable<PlayerScore> results, PlayerScore player)
        {
            var totalOfWorstScores = new Points(0.0);

            var allResultsForPlayer =
                results.Where(x => x.Name == player.Name)
                    .Select(x => x.Points)
                    .OrderByDescending(x => x)
                    .ToList();

            var discardPolicy = new DiscardPolicy(0, 0);

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
                    totalOfWorstScores += allResultsForPlayer[i];
                }
            }

            return totalOfWorstScores;
        }
    }
}