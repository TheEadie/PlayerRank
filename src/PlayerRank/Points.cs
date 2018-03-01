using System;

namespace PlayerRank
{
    public class Points : IComparable
    {
        private readonly double _points;

        public Points(double points)
        {
            _points = points;
        }

        public double GetValue()
        {
            return _points;
        }

        protected bool Equals(Points other)
        {
            return _points.Equals(other._points);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Points) obj);
        }

        public override int GetHashCode()
        {
            return _points.GetHashCode();
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
            return new Points(pointsA._points + pointsB._points);
        }

        public static Points operator -(Points pointsA, Points pointsB)
        {
            return new Points(pointsA._points - pointsB._points);
        }

        public static Points operator /(Points points, Points divider)
        {
            return new Points(points._points / divider._points);
        }

        public static double operator /(Points points, double divider)
        {
            return points._points / divider;
        }

        public static Points operator *(Points points, Points divider)
        {
            return new Points(points._points * divider._points);
        }

        public static bool operator >(Points pointsA, Points pointsB)
        {
            return (pointsA._points > pointsB._points);
        }

        public static bool operator <(Points pointsA, Points pointsB)
        {
            return (pointsA._points < pointsB._points);
        }

        public static bool operator ==(Points pointsA, Points pointsB)
        {
            if (ReferenceEquals(pointsA, null) && ReferenceEquals(pointsB, null))
            {
                return true;
            }

            if (ReferenceEquals(pointsA, null))
            {
                return false;
            }

            if (ReferenceEquals(pointsB, null))
            {
                return false;
            }

            return (pointsA._points == pointsB._points);
        }

        public static bool operator !=(Points pointsA, Points pointsB)
        {
            return !(pointsA == pointsB);
        }

        public static double Pow(double doubleA, Points pointsB)
        {
            return Math.Pow(doubleA, pointsB._points);
        }

        public override string ToString()
        {
            return _points.ToString();
        }
    }
}