using System.Collections.Generic;

namespace ELORank
{
    public class League
    {
        private readonly List<Player> m_Players = new List<Player>();

        public void AddPlayer(string playerName)
        {
            var player = new Player(playerName);

            m_Players.Add(player);
        }

        public List<Player> GetLeaderBoard()
        {
            return m_Players;
        }
    }
}