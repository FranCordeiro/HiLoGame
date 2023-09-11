using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingServer
{
    public class PlayerStatus
    {
        public int Score { get; set; }
        public int Attempts { get; set; }

        public PlayerStatus()
        {
            Score = 0;
            Attempts = 0;
        }

        public void IncreaseScore()
        {
            Score++;
        }

        public void IncreaseAttempts()
        {
            Attempts++;
        }
    }
}
