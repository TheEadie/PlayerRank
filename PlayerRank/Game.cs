using System.Collections.Generic;

namespace ELORank
{
    public class Game
    {
        private readonly Dictionary<string, double> m_Scores = new Dictionary<string, double>();

        public void AddResult(string name, double score)
        {
            if (m_Scores.ContainsKey(name))
            {
                m_Scores[name] = score;
            }
            else
            {
                m_Scores.Add(name, score);
            }
        }

        internal Dictionary<string, double> GetResults()
        {
            return m_Scores;
        }
    }
}