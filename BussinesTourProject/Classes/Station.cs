using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BussinesTourProject.Classes
{
    public class Station
    {
        public Image StationImage;
        public int costToBuy;
        public int value;
        public Player playerOwnerStation;

        public Station(Image StationImage, int costToBuy)
        {
            this.StationImage = StationImage;
            this.costToBuy = costToBuy;
            this.value = 0;
        }

    }
}
