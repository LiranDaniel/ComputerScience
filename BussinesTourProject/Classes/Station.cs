using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BussinesTourProject.Classes
{
    public class Station : Property
    {
        public int playerStations;

        public Station() : base (basicCostToBuy: 200_000, basicCostToPayRent: 50_000,
            levelUpgradeRent: 50_000)
        {
        
        }


       /* public void StationBought(Player player)
        {
            
        }
        public static void ManyStation(Player player)
        {

            foreach(object obj in GameManager.ArrayMap)
            {
                if (obj is Station)
                {
                    if(((Station)obj).playerOwnerStation == player)
                        
                }
            }
           

        }
       */
    }
}
