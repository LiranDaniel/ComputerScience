using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using static BussinesTourProject.Classes.House;
using Windows.UI.Xaml.Media.Imaging;

namespace BussinesTourProject.Classes
{
    public class Station : Property
    {

        public Station() : base (basicCostToBuy: 200_000, basicCostToPayRent: 50_000,
            levelUpgradeRent: 50_000) { }


        public void PropertyUpgrade()
        {
            int LastCostToBuy = currentCostToBuy;
            currentCostToBuy = basicCostToBuy * ownerOfTheProperty.playerStations;
            currentCostToPayRent = basicCostToPayRent + ((ownerOfTheProperty.playerStations - 1) * levelUpgradeRent);
            double txtDisplay = currentCostToPayRent;
            int times = 0;
            ownerOfTheProperty.amountOfMoney -= (currentCostToBuy - LastCostToBuy);
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
            string formattedNumber = ownerOfTheProperty.amountOfMoney.ToString("N0"); // adding 
            ownerOfTheProperty.txtMoney.Text = $"{formattedNumber}$";
        }
        public void BuyProperty()
        {
            ownerOfTheProperty = GameManager.currentPlayer;
            ownerOfTheProperty.playerStations++;
            PropertyUpgrade();
        }

    }
}
