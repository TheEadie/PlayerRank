using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRank
{
    public class Game
    {
        private readonly Dictionary<string, Points> m_Points = new Dictionary<string, Points>();
        
        public void AddResult(string name, Points points)
        {
            if (m_Points.ContainsKey(name))
            {
                m_Points[name] = points;
            }
            else
            {
                m_Points.Add(name, points);
            }
        }

        public void AddResult(string name, Position points)
        {
            throw new NotImplementedException();
        }

        internal Dictionary<string, Points> GetGameResults()
        {
            return m_Points;
        }

        /// Obsolete V1 API

        [Obsolete("Please use AddResult(string, Points) instead")]
        public void AddResult(string name, double score)
        {
            if (m_Points.ContainsKey(name))
            {
                m_Points[name] = new Points(score);
            }
            else
            {
                m_Points.Add(name, new Points(score));
            }
        }
        
        [Obsolete("Please use GetGameResults() instead")]
        internal Dictionary<string, double> GetResults()
        {
            var oldResults = new Dictionary<string, double>();
            foreach (var score in m_Points)
            {
                oldResults.Add(score.Key, score.Value.GetValue());
            }
            return oldResults;
        }
    }
}