using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingServer
{
    public class GameSettings
    {
        public int MinValue { get; }
        public int MaxValue { get; }
        public int MagicNumber { get; set; }

        public GameSettings(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            SetNewMagicNumber();
        }

        public void SetNewMagicNumber()
        {
            MagicNumber = new Random().Next(MinValue, MaxValue + 1);
        }

    }
}
