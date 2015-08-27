namespace PlayerRank
{
    public class Position
    {
        private readonly int m_Position;
        
        public Position(int position)
        {
            m_Position = position;
        }

        protected bool Equals(Position other)
        {
            return m_Position.Equals(other.m_Position);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            return m_Position.GetHashCode();
        }
    }
}