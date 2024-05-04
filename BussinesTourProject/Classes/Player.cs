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
        public int amountOfMoney;
        public string name;
        public string imgName;
        public Image Img;
        public state[] playerState;
        public int turnsStackJail;
        public TextBlock txtMoney;
        public TextBlock txtState;

        public enum state
        {
            forward, backward, right, left
        }
        public Player(string imgName, state[] playerState, string name)
        {
            this.imgName = imgName;
            this.name = name;
            this.currentPosition = 0;
            amountOfMoney = 2_000_000;
            this.playerState = playerState;
            this.turnsStackJail = 0;
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
            if (currentPosition == 8)
                this.turnsStackJail = 3;
        }

        public void ChangePlayerImageByEnumValue(int stateIndex)
        {

            switch (playerState[stateIndex])
            {
                case Player.state.left:

                    Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + imgName + "/RedCarLeft.png"));
                    Grid.SetColumnSpan(Img, 6);
                    Grid.SetRowSpan(Img, 3);
                    break;

                case Player.state.right:

                    Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + imgName + "/RedCarRight.png"));
                    Grid.SetColumnSpan(Img, 6);
                    Grid.SetRowSpan(Img, 3);
                    break;

                case Player.state.forward:

                    Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + imgName + "/RedCarForward.png"));
                    Grid.SetColumnSpan(Img, 3);
                    Grid.SetRowSpan(Img, 5);
                    break;

                case Player.state.backward:

                    Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + imgName + "/RedCarBackward.png"));
                    Grid.SetColumnSpan(Img, 3);
                    Grid.SetRowSpan(Img, 5);
                    break;
            }
        }


        public int CalculatePropertyValue()
        {
            int value = 0;

            foreach (object obj in GameManager.ArrayMap )
            {
                if( obj != null )
                {
                    if ( obj is House ) {
                        if (((House)obj).ownerOfTheProperty == GameManager.currentPlayer)
                            value += ((House)obj).currentCostToPayRent;
                    }
                    else if ( obj is Station ) { }
                }
            }
            return value;
        }

    }

    
}
