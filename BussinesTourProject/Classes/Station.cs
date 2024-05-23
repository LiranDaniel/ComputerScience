﻿using System;
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


        /// <summary>
        /// Increase Station value if there is more than one station
        /// that the current player owns
        /// </summary>
        private void IncreaseValuesStation()
        {
            foreach(object obj in GameManager.ArrayMap)
            {
                if (obj is Station & obj != null)
                {
                    if (((Station)obj).ownerOfTheProperty == GameManager.currentPlayer)
                    {
                        ((Station)obj).currentCostToPayRent = currentCostToPayRent;
                        double txtDisplay = ((Station)obj).currentCostToPayRent;
                        int times = 0;
                        ownerOfTheProperty.amountOfMoney -= currentCostToBuy;
                        while (txtDisplay > 1000)
                        {
                            txtDisplay = txtDisplay / 1000;
                            times++;
                        }
                        if (times == 1)
                            ((Station)obj).txtOfMoneyDisplayRent.Text = $"{txtDisplay}K";
                        else if (times == 2)
                            ((Station)obj).txtOfMoneyDisplayRent.Text = $"{txtDisplay}M";
                        else
                            ((Station)obj).txtOfMoneyDisplayRent.Text = $"{txtDisplay}";
                    }
                }
            }
        }

        /// <summary>
        /// Upgrade the station
        /// </summary>
        public void PropertyUpgrade()
        {
            currentLevel = ownerOfTheProperty.playerStations;
            currentCostToBuy = basicCostToBuy;
            currentCostToPayRent = CalculatePayRentByLevel();
            double txtDisplay = currentCostToPayRent;
            int times = 0;
            ownerOfTheProperty.amountOfMoney -= currentCostToBuy;
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

            IncreaseValuesStation();
            GameManager.MoneySoundPlayer.Play();

        }

        /// <summary>
        /// Buy for the current player the current station
        /// </summary>
        public void BuyProperty()
        {
            ownerOfTheProperty = GameManager.currentPlayer;
            ownerOfTheProperty.playerStations++;
            PropertyUpgrade();
        }

    }
}
