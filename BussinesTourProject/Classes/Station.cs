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

    }
}
