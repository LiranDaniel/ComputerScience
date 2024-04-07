using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace BussinesTourProject.Classes
{
    public class House
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
                {HouseState.None, @"None"},
                {HouseState.BasicHouse, @"/Assets\Images\SquareImages\House1.png"},
                {HouseState.AdvanceHouse, @"/Assets\Images\SquareImages\House2.png"},
                {HouseState.villa, @"/Assets\Images\SquareImages\House3.png"},
                {HouseState.Hotel, @"/Assets\Images\SquareImages\House4.png"}
        };

        public static int[] arrayTimesValue = { 0, 1, 2, 3, 4 };
        public Image HouseImage;
        public int basicValue;
        public int currentValue;
        public HouseState houseCurrentState;
        public Player playerOwnerHouse;

        public House(int basicValue)
        {
            this.basicValue = basicValue;
            this.currentValue = 0;
        }
        public void ChangeHouse(int houseUpgradeState)
        {   
            HouseImage.Source = new BitmapImage(new Uri($"ms-appx://" + filePathImages[(HouseState)houseUpgradeState]));
            currentValue = basicValue * arrayTimesValue[houseUpgradeState];
        }
        
    }
}
