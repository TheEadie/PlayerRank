using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRank
{
    public class Game
    {
        private readonly Dictionary<string, Points> m_Scores = new Dictionary<string, Points>();

        public void AddResult(string name, double score)
        {
            if (m_Scores.ContainsKey(name))
            {
                m_Scores[name] = new Points(score);
            }
            else
            {
                m_Scores.Add(name, new Points(score));
            }
        }

        public void AddResult(string name, Points points)
        {
            if (m_Scores.ContainsKey(name))
            {
                m_Scores[name] = points;
            }
            else
            {
                m_Scores.Add(name, points);
            }
        }

        internal Dictionary<string, double> GetResults()
        {
            var oldResults = new Dictionary<string, double>();
            foreach (var score in m_Scores)
            {
                oldResults.Add(score.Key, score.Value.GetValue());
            }
            return oldResults;
        }
    }
}