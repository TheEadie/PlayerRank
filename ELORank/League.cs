using System.Collections.Generic;
using System.Linq;

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
            return m_Players.OrderBy(x => x.Score).ToList();
        }

        public void RecordGame(Game game)
        {
            foreach (var result in game.GetResults())
            {
                GetPlayer(result.Key).AddScore(result.Value);
            }
        }

        private Player GetPlayer(string name)
        {
            return m_Players.SingleOrDefault(player => player.Name == name);
        }
    }
}