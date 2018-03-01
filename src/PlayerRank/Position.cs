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

        private readonly int _position;
        
        public Position(int position)
        {
            _position = position;
        }

        public int GetValue()
        {
            return _position;
        }

        protected bool Equals(Position other)
        {
            return _position.Equals(other._position);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            return _position.GetHashCode();
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
            return (pointsA._position < pointsB._position);
        }

        public static bool operator <(Position pointsA, Position pointsB)
        {
            return (pointsA._position > pointsB._position);
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

            return (positionA._position == positionB._position);
        }

        public static bool operator !=(Position positionA, Position positionB)
        {
            return !(positionA == positionB);
        }

        public override string ToString()
        {
            return _position.ToString();
        }
    }
}