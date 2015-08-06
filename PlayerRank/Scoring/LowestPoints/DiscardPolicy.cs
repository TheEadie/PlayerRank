namespace PlayerRank.Scoring.LowestPoints
{
    public class DiscardPolicy
    {
        public int NumberOfdiscards { get; private set; }
        public int GamesToBePlayed { get; private set; }
        
        public DiscardPolicy(int numberOfdiscards, int gamesToBePlayed)
        {
            NumberOfdiscards = numberOfdiscards;
            GamesToBePlayed = gamesToBePlayed;
        }
    }
}