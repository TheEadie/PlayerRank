namespace PlayerRank.Scoring
{
    public class Discard
    {
        public int NumberOfdiscards { get; private set; }
        public int GamesToBePlayed { get; private set; }
        
        public Discard(int numberOfdiscards, int gamesToBePlayed)
        {
            NumberOfdiscards = numberOfdiscards;
            GamesToBePlayed = gamesToBePlayed;
        }
    }
}