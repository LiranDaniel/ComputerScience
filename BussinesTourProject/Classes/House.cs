using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace BussinesTourProject.Classes
{
    public class Houses
    {
        public enum HouseState
        {
            None = 0,
            BasicHouse = 1,
            AdvanceHouse = 2,
            villa = 3, 
            Hotel = 4
        }
        public static Dictionary<HouseState, string> filePathImages = new Dictionary<HouseState, string>() {
                {HouseState.None, @"None" },
                {HouseState.BasicHouse, @"/Assets\Images\SquareImages\House1.png"},
                { HouseState.AdvanceHouse, @"/Assets\Images\SquareImages\House2.png"},
                {HouseState.villa, @"/Assets\Images\SquareImages\House3.png"},
                {HouseState.Hotel, @"/Assets\Images\SquareImages\House4.png" } };

        private static int[] arrayTimesValue = { 0, 1, 2, 3, 4 };
        public Image HouseImage;
        private int basicValue;
        private int currentValue;
        public HouseState da;

        public Houses(int basicValue)
        {
            this.basicValue = basicValue;
        }
        public void ChangeHouse(int houseUpgradeState)
        {   
            HouseImage.Source = new BitmapImage(new Uri($"ms-appx://" + filePathImages[(HouseState)houseUpgradeState]));
            currentValue = basicValue * arrayTimesValue[houseUpgradeState];
        }
    }
}
