using System;

namespace PlayerRank
{
    public class Points : IComparable
    {
        private readonly double m_Points;

        public Points(double points)
        {
            m_Points = points;
        }

        [Obsolete("This getter will be removed in a future version")]
        internal double GetValue()
        {
            return m_Points;
        }

        protected bool Equals(Points other)
        {
            return m_Points.Equals(other.m_Points);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Points) obj);
        }

        public override int GetHashCode()
        {
            return m_Points.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            var other = obj as Points;

            if (other == null)
                throw new ArgumentException("Can not compare Points to other type");

            if (other > this)
                return -1;
            if (other < this)
                return 1;

            return 0;

        }

        public static Points operator +(Points pointsA, Points pointsB)
        {
            return new Points(pointsA.m_Points + pointsB.m_Points);
        }

        public static Points operator -(Points pointsA, Points pointsB)
        {
            return new Points(pointsA.m_Points - pointsB.m_Points);
        }

        public static double operator /(Points points, double divider)
        {
            return points.m_Points / divider;
        }

        public static bool operator >(Points pointsA, Points pointsB)
        {
            return (pointsA.m_Points > pointsB.m_Points);
        }

        public static bool operator <(Points pointsA, Points pointsB)
        {
            return (pointsA.m_Points < pointsB.m_Points);
        }

        public static bool operator ==(Points pointsA, Points pointsB)
        {
            return (pointsA.m_Points == pointsB.m_Points);
        }

        public static bool operator !=(Points pointsA, Points pointsB)
        {
            return !(pointsA == pointsB);
        }

        public override string ToString()
        {
            return m_Points.ToString();
        }
    }
}