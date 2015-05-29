namespace PlayerRank.Scoring
{
    public interface IScoringStrategy
    {
        void NewPlayer(Player player);
        void UpdateScores(League league, Game game);
    }
}