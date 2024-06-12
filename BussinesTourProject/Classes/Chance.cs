using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesTourProject.Classes
{
    public class Chance
    {
        public static string TextDisplayed;
        
        public static void GetMoney()
        {
            TextDisplayed = "Congratulations You\n Won The lottery!!!\nYou are getting 300K";
            GameManager.currentPlayer.AmountOfMoneyChange(-300_000);
            GameManager.txtCardGrid.Text = TextDisplayed;
            GameManager.UICardGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        public static void GoToWorldTourSquare()
        {
            TextDisplayed = "You Won A Free Ticket\nNow You Can Fly Into\nOther Squares In The Map";
        }

        public static void LoseMoney()
        {
            TextDisplayed = "Unlucky You Lose\nYou Have To Pay To \nThe Bank 100K\n";
        }

        public static void SelectPropertyWorldChampion()
        {
            TextDisplayed = "You Can Select Property\nTo Hold The WorldChampionShip";
        }
    }
}
