using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BussinesTourProject.Classes
{
    public class Property
    {
        public int basicCostToBuy;          // If someone want to pay property this is the basic amout of money that he need to pay
        public int basicCostToPayRent;      // If someone land on that property this is the basic amout of money that he need to pay
        public int levelUpgradeRent;        // If the property level has upgraded then between every upgrade level this is the jump of the rent price
        public int currentCostToBuy;
        public int currentCostToPayRent;
        public TextBlock txtOfMoneyDisplayRent;

        public Player ownerOfTheProperty;   // This is the owner of the property
        public Image imageOfProperty;

        public Property(int basicCostToBuy, int basicCostToPayRent, int levelUpgradeRent)
        {
            this.basicCostToBuy = basicCostToBuy;
            this.basicCostToPayRent = basicCostToPayRent; 
            this.levelUpgradeRent = levelUpgradeRent;
            this.currentCostToBuy = basicCostToBuy;
            this.currentCostToPayRent = basicCostToPayRent;
        }
        public void PropertyUpgrade(int level)
        {
            currentCostToPayRent += level * basicCostToPayRent;
            currentCostToBuy = level * basicCostToBuy;

            int txtDisplay = currentCostToPayRent;
            int times = 0;

            while (txtDisplay < 1000)
            {
                txtDisplay /= 1000;
                times++;
            }
            if (times == 1) 
                txtOfMoneyDisplayRent.Text = $"{txtDisplay}K";
            else
                txtOfMoneyDisplayRent.Text = $"{txtDisplay}M";

        }












        //public Property(int basicCostToBuy, int basicCostToPayRent, int levelUpgradeRent, Image imageOfProperty)
        //{
        //    this.basicCostToBuy = basicCostToBuy;
        //    this.basicCostToPayRent = basicCostToPayRent; 
        //    this.levelUpgradeRent = levelUpgradeRent;
        //    this.imageOfProperty = imageOfProperty;
        //}

        public void BuyProperty(Player player)
        {
            player.AmountOfMoney -= basicCostToBuy;
            ownerOfTheProperty = player;
        }
    }
}
