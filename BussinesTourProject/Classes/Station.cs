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

        public Station(Image StationImagem, int costToBut)
        {
            this.StationImage = StationImagem;
            this.costToBuy = costToBut;
            this.value = 0;
        }

    }
}
