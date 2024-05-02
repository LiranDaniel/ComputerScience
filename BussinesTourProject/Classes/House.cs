﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace BussinesTourProject.Classes
{
    public class House : Property
    {
        public enum HouseState
        {
            None = 0,
            BasicHouse = 1,
            AdvanceHouse = 2,
            villa = 3, 
            Hotel = 4
        }
        public static Dictionary<HouseState, string> filePathImageHouses = new Dictionary<HouseState, string>() {
                {HouseState.None, null},
                {HouseState.BasicHouse, @"/Assets\Images\SquareImages\House1.png"},
                {HouseState.AdvanceHouse, @"/Assets\Images\SquareImages\House2.png"},
                {HouseState.villa, @"/Assets\Images\SquareImages\House3.png"},
                {HouseState.Hotel, @"/Assets\Images\SquareImages\House4.png"}
        };

        public HouseState houseCurrentState;
        public int levelUpgradePrice; // If you upgrade your house then you need to pay that amount of money between every level upgrade
        public int currentCostToPayRent;

        public House(int basicCostToBuy, int basicCostToPayRent, int levelUpgradeRent, int levelUpgradePrice) : 
            base(basicCostToBuy, basicCostToPayRent, levelUpgradeRent)
        {
            this.levelUpgradePrice = levelUpgradePrice;
            this.houseCurrentState = HouseState.None;
        }








        //public void ChangeHouse(int houseUpgradeLevel)
        //{
        //    imageOfProperty.Source = new BitmapImage(new Uri($"ms-appx://" + filePathImageHouses[(HouseState)houseUpgradeLevel]));
        //    houseCurrentState = (HouseState)houseUpgradeLevel;
        //    currentValue = basicValue + LevelUpgradePrice;
        //}
        
    }
}
