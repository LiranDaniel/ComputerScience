using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BussinesTourProject.Classes
{
    public class Player
    {
        // Matrix.GetLenght(0) == Number of rows
        // Matrix.GetLenght(1) == Number of column 
        public const int MaxPosition = 30;
        public int[,] PlayerPosition;
        public int currentPosition;
        public int AmountOfMoney;
        public string name;
        public List<int> listHouses;
        public Image Img;

        public Player(string name)
        {
            this.name = name;
            this.currentPosition = 0;
            AmountOfMoney = 2_000_000;
            listHouses = new List<int>();
        }

        public void SetPlayerPosition(int[,] PlayerPosition)
        {
            this.PlayerPosition = PlayerPosition;
        }
        public void ChangePlayerPosition(int diceResult)
        {
            currentPosition += diceResult;
            if (currentPosition > PlayerPosition.GetLength(1) - 1)
            {
                currentPosition = currentPosition - PlayerPosition.GetLength(1);
            }
        }

    }
}
