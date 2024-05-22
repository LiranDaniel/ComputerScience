using System;
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
                {HouseState.BasicHouse, @"House1.png"},
                {HouseState.AdvanceHouse, @"House2.png"},
                {HouseState.villa, @"House3.png"},
                {HouseState.Hotel, @"House4.png"}
        };

        public HouseState houseCurrentState; // hold the current house level state
        public int levelUpgradePrice; // If you upgrade your house then you need to pay that amount of money between every level upgrade

        public House(int basicCostToBuy, int basicCostToPayRent, int levelUpgradeRent, int levelUpgradePrice) : 
            base(basicCostToBuy, basicCostToPayRent, levelUpgradeRent)
        {
            this.levelUpgradePrice = levelUpgradePrice;
            this.houseCurrentState = HouseState.None;
        }

        /// <summary>
        /// when upgrading the house updateing the display rent price, image,
        /// and decrease from the player amount of money the price of the upgrade.
        /// updating the currentPriceToPay and the currentBuyingPrice
        /// </summary>
        /// <param name="level"></param>
        public void PropertyUpgrade(int level)
        {
            currentLevel = level;
            houseCurrentState = (HouseState)(level);
            int LastCostToBuy = currentCostToBuy;
            currentCostToPayRent = ((level - 1) * levelUpgradeRent) + basicCostToPayRent;
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
            imageOfProperty.Source = new BitmapImage(new Uri($@"ms-appx:///Assets\Images\SquareImages\HousesIcons\{ownerOfTheProperty.playerNumber}\{House.filePathImageHouses[houseCurrentState]}"));
            string formattedNumber = ownerOfTheProperty.amountOfMoney.ToString("N0"); // adding 
            ownerOfTheProperty.txtMoney.Text = $"{formattedNumber}$";
        }

        /// <summary>
        /// buys for the player the property
        /// </summary>
        /// <param name="level"></param>
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
