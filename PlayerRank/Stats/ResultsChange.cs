namespace PlayerRank.Stats
{
    public class ResultsChange
    {
        public string Name { get; private set; }
        public int PositionChange { get; private set; }
        public double PointsChange { get; private set; }

        public ResultsChange(string name, int positionChange, double pointsChange)
        {
            PositionChange = positionChange;
            PointsChange = pointsChange;
            Name = name;
        }
    }
}