using System;

namespace PlayerRank
{
    public class Position : IComparable
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

        public int CompareTo(object obj)
        {
            var other = obj as Position;

            if (other == null)
                throw new ArgumentException("Can not compare Points to other type");

            if (other > this)
                return -1;
            if (other < this)
                return 1;

            return 0;

        }

        public static bool operator >(Position pointsA, Position pointsB)
        {
            return (pointsA.m_Position > pointsB.m_Position);
        }

        public static bool operator <(Position pointsA, Position pointsB)
        {
            return (pointsA.m_Position < pointsB.m_Position);
        }
    }
}