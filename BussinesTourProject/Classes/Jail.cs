﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BussinesTourProject.Classes
{
    public class Jail
    {
        public static TextBlock txtRemaningRoundJail;
        public static void PayToLeave()
        {
            GameManager.currentPlayer.amountOfMoney -= 200_000;
            GameManager.currentPlayer.turnsStackJail = 0;
        }
        
    }
}