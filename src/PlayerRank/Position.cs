using System;

namespace PlayerRank
{
    public class Position : IComparable<Position>
    {
        // Helper properties for more fluent api
        public static readonly Position First = new Position(1);
        public static readonly Position Second = new Position(2);
        public static readonly Position Third = new Position(3);
        public static readonly Position Fourth = new Position(4);
        public static readonly Position Fifth = new Position(5);
        public static readonly Position Sixth = new Position(6);
        public static readonly Position Seventh = new Position(7);
        public static readonly Position Eighth = new Position(8);
        public static readonly Position Ninth = new Position(9);
        public static readonly Position Tenth = new Position(10);

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

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            return _position.GetHashCode();
        }

        public int CompareTo(Position other)
        {
            if (other > this)
                return -1;
            return other < this ? 1 : 0;
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