namespace PlayerRank.Scoring
{
    public class SimpleScoringStrategy : IScoringStrategy
    {
        public void NewPlayer(Player player)
        {
            player.Score = 0;
        }

        public void UpdateScores(League league, Game game)
        {
            foreach (var result in game.GetResults())
            {
                var player = league.GetPlayer(result.Key);

                if (player == null)
                {
                    league.AddPlayer(result.Key);
                }
                
                league.GetPlayer(result.Key).AddScore(result.Value);
                
            }
        }
    }
}