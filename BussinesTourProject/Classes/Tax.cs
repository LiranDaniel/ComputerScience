using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesTourProject.Classes
{
    public class Tax
    {
        /// <summary>
        /// Calculate the amount of money that the current player have to pay if he lands on the tax square
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static int CalculateTaxes(Player player)
        {
            int taxes = player.amountOfMoney;

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
