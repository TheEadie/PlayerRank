namespace ELORank
{
    public interface IScoringStrategy
    {
        void UpdateScores(League league, Game game);
    }
}