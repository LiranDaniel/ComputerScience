using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesTourProject.Classes
{
    public static class WorldChampion
    {
        public static double WorldChampionTimes = 1.5; // Time world champion ships times its currentretPrice of the house
        public static Property PropertyHoldingWorldChampion;

        /// <summary>
        /// Increase the amount of times that the world championships increase the price of a house
        /// </summary>
        public static void IncreaseWorldChampion()
        {
            WorldChampionTimes += 0.5;
        }
    }
}
