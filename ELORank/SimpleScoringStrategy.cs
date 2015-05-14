namespace ELORank
{
    public class SimpleScoringStrategy : IScoringStrategy
    {
        public void UpdateScores(League league, Game game)
        {
            foreach (var result in game.GetResults())
            {
                league.GetPlayer(result.Key).AddScore(result.Value);
            }
        }
    }
}