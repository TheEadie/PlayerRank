using System;

namespace PlayerRank
{
    public class Position : IComparable
    {
        // Helper properties for more fluent api
        public static Position First = new Position(1);
        public static Position Second = new Position(2);
        public static Position Third = new Position(3);
        public static Position Fourth = new Position(4);
        public static Position Fifth = new Position(5);
        public static Position Sixth = new Position(6);
        public static Position Seventh = new Position(7);
        public static Position Eighth = new Position(8);
        public static Position Ninth = new Position(9);
        public static Position Tenth = new Position(10);

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
            return (pointsA.m_Position < pointsB.m_Position);
        }

        public static bool operator <(Position pointsA, Position pointsB)
        {
            return (pointsA.m_Position > pointsB.m_Position);
        }

        public static bool operator ==(Position positionA, Position positionB)
        {
            if (ReferenceEquals(positionA, null) && ReferenceEquals(positionB, null))
            {
                return true;
            }

            if (ReferenceEquals(positionA, null))
            {
                return false;
            }

            if (ReferenceEquals(positionB, null))
            {
                return false;
            }

            return (positionA.m_Position == positionB.m_Position);
        }

        public static bool operator !=(Position positionA, Position positionB)
        {
            return !(positionA == positionB);
        }

        public override string ToString()
        {
            return m_Position.ToString();
        }
    }
}