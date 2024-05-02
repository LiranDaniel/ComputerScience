﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesTourProject.Classes
{
    public class Tax
    {
        public static int CalculateTaxes(Player player)
        {
            int taxes = player.AmountOfMoney;

            foreach(object obj in GameManager.ArrayMap)
            {
                if (obj is Property)
                {
                    if (((Property)obj).ownerOfTheProperty == player)
                        taxes += ((Property)obj).currentCostToBuy;
                }    
            }
                
            return taxes / 10;
        }
    }
}
