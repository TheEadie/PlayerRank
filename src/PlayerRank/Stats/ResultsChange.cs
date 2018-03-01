namespace PlayerRank.Stats
{
    public class ResultsChange
    {
        public string Name { get; }
        public Position Position { get; }
        public int PositionChange { get; }
        public Points Points { get; }
        public double PointsChange { get; }

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