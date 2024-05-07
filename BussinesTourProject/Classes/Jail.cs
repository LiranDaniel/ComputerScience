using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BussinesTourProject.Classes
{
    public class Jail
    {
        public static TextBlock txtRemaningRoundJail; // the Text Blocks object that hold the txt that display the remaning round on jail of the current player

        /// <summary>
        /// Pay to leave function that being used to leave the jail by paying money
        /// </summary>
        public static void PayToLeave()
        {
            GameManager.currentPlayer.amountOfMoney -= 200_000;
            GameManager.currentPlayer.turnsStackJail = 0;
        }
        
    }
}
