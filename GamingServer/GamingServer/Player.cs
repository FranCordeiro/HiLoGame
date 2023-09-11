using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingServer
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlayerStatus Status { get; set; }

        public Player()
        {
            Status = new PlayerStatus();
        }
    }
}
