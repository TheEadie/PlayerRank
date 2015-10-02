namespace PlayerRank.Stats
{
    public class ResultsChange
    {
        public string Name { get; private set; }
        public Position Position { get; private set; }
        public int PositionChange { get; private set; }
        public Points Points { get; private set; }
        public double PointsChange { get; private set; }

        public ResultsChange(string name, Position position, int positionChange, Points points, double pointsChange)
        {
            PositionChange = positionChange;
            PointsChange = pointsChange;
            Position = position;
            Points = points;
            Name = name;
        }
    }
}