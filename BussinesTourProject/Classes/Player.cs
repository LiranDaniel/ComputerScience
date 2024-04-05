using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace BussinesTourProject.Classes
{
    public class Player
    {
        // Matrix.GetLenght(0) == Number of rows
        // Matrix.GetLenght(1) == Number of column 
        public const int MaxPosition = 32;
        public int[,] PlayerPosition;
        public int currentPosition;
        public int AmountOfMoney;
        public string name;
        public string imgName;
        public List<int> listHouses;
        public Image Img;
        public state[] playerState;

        public enum state
        {
            forward, backward, right, left
        }
        public Player( string imgName, state[] playerState)
        {
            this.imgName = imgName;
            this.name = "Admin";
            this.currentPosition = 0;
            AmountOfMoney = 2_000_000;
            listHouses = new List<int>();
            this.playerState = playerState;
        }

        private void MovingPlayerPosition(int diceResult)
        {
            int currentPosition = this.currentPosition + 1;
            this.ChangePlayerPosition(diceResult); // changing position of the player, and make sure that there is not overflow
            for (int i = 0; i < diceResult; i++)
            {

                while (currentPosition > (this.PlayerPosition.GetLength(1) - 1))
                {
                    currentPosition -= this.PlayerPosition.GetLength(1);
                }
                if (currentPosition >= 11 && currentPosition < 20)
                    this.Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/Player1/RedCarBackward.png"));

                Grid.SetRow(this.Img, this.PlayerPosition[0, currentPosition]);
                Grid.SetColumn(this.Img, this.PlayerPosition[1, currentPosition]);

                currentPosition++;
            }
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

        public void ChangePlayerImageByEnumValue(state state)
        {

            switch (state)
            {
                case Player.state.left:
                    Console.WriteLine();
                    break;
                case Player.state.right:
                    Console.WriteLine();
                    break;
                case Player.state.forward:
                    Console.WriteLine();
                    break;
                case Player.state.backward:
                    Console.WriteLine();
                    break;
            }
        }

    }

    
}
