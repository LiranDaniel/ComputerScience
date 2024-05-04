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
                {HouseState.BasicHouse, @"/Assets\Images\SquareImages\HousesIcons\House1.png"},
                {HouseState.AdvanceHouse, @"/Assets\Images\SquareImages\HousesIcons\House2.png"},
                {HouseState.villa, @"/Assets\Images\SquareImages\HousesIcons\House3.png"},
                {HouseState.Hotel, @"/Assets\Images\SquareImages\HousesIcons\House4.png"}
        };

        public HouseState houseCurrentState;
        public int levelUpgradePrice; // If you upgrade your house then you need to pay that amount of money between every level upgrade

        public House(int basicCostToBuy, int basicCostToPayRent, int levelUpgradeRent, int levelUpgradePrice) : 
            base(basicCostToBuy, basicCostToPayRent, levelUpgradeRent)
        {
            this.levelUpgradePrice = levelUpgradePrice;
            this.houseCurrentState = HouseState.None;
        }


        public void PropertyUpgrade(int level)
        {
            houseCurrentState = (HouseState)level;
            int LastCostToBuy = currentCostToBuy;
            currentCostToPayRent = (level * levelUpgradeRent) + basicCostToPayRent;
            currentCostToBuy = basicCostToBuy + (level * levelUpgradePrice);
            ownerOfTheProperty.amountOfMoney -= (currentCostToBuy - LastCostToBuy);
            double txtDisplay = currentCostToPayRent;
            int times = 0;

            while (txtDisplay > 1000)
            {
                txtDisplay = txtDisplay / 1000;
                times++;
            }
            if (times == 1)
                txtOfMoneyDisplayRent.Text = $"{txtDisplay}K";
            else if (times == 2)
                txtOfMoneyDisplayRent.Text = $"{txtDisplay}M";
            else
                txtOfMoneyDisplayRent.Text = $"{txtDisplay}";
            imageOfProperty.Source = new BitmapImage(new Uri($"ms-appx://{House.filePathImageHouses[houseCurrentState]}"));
            ownerOfTheProperty.txtMoney.Text = $"{ownerOfTheProperty.amountOfMoney}";
        }
        public void BuyProperty(int level)
        {
            ownerOfTheProperty = GameManager.currentPlayer;
            PropertyUpgrade(level);
        }




        //public void ChangeHouse(int houseUpgradeLevel)
        //{
        //    imageOfProperty.Source = new BitmapImage(new Uri($"ms-appx://" + filePathImageHouses[(HouseState)houseUpgradeLevel]));
        //    houseCurrentState = (HouseState)houseUpgradeLevel;
        //    currentValue = basicValue + LevelUpgradePrice;
        //}

    }
}
