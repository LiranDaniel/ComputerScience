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
        public const int MaxPosition = 32; // max amount of squares in the map
        public int[,] PlayerPosition;      // array of all the position of the player
        public int currentPosition;        // the current position of the player on the map
        public int amountOfMoney;          // the amount of money that the player have
        public string name;                // player name
        public string imgName;             // player img name
        public string playerNumber;        // the number of the player
        public Image Img;                  // the Image object of the player in the game
        public state[] playerState;        // the state of the player on the special squares at the map
        public int turnsStackJail;         // how much turns remain him to be stuck in jail
        public TextBlock txtMoney;         // the Text block object that display his amount of money in the game
        public TextBlock txtState;         // the Text block object that display his state in the game
        public int playerStations = 0;     // the amount of Station that he have

        public enum state
        {
            forward, backward, right, left
        }

        public Player(string playerNumber, string imgName, state[] playerState, string name)
        {
            this.playerNumber = playerNumber;
            this.imgName = imgName;
            this.name = name; 
            this.currentPosition = 0;
            amountOfMoney = 2_000_000;
            this.playerState = playerState;
            this.turnsStackJail = 0;
        }
        public Player(string playerNumber, string imgName, state[] playerState, string name, Image img, int[,] matrixPlayerPosition)
        {
            this.PlayerPosition = matrixPlayerPosition;
            this.playerNumber = playerNumber;
            this.imgName = imgName;
            this.name = name;
            this.currentPosition = 0;
            amountOfMoney = 2_000_000;
            this.playerState = playerState;
            this.turnsStackJail = 0;
            this.Img = img;
        }

        /// <summary>
        /// just setting player position array
        /// </summary>
        /// <param name="PlayerPosition"></param>
        public void SetPlayerPosition(int[,] PlayerPosition)
        {
            this.PlayerPosition = PlayerPosition;
        }

        /// <summary>
        /// Change player positon
        /// </summary>
        /// <param name="diceResult"></param>
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

        /// <summary>
        /// when you are going thought the special square basickly it those is just 
        /// chaning the player angel the appropriate one that specific square
        /// </summary>
        /// <param name="stateIndex"></param>
        public void ChangePlayerImageByEnumValue(int stateIndex)
        {

            switch (playerState[stateIndex])
            {
                case Player.state.left:
                    //                                                             /Players/Player1/RedCarForward.png
                    Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + playerNumber + "/" + imgName + "Left.png"));
                    Grid.SetColumnSpan(Img, 6);
                    Grid.SetRowSpan(Img, 3);
                    break;

                case Player.state.right:

                    Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + playerNumber + "/" + imgName + "Right.png"));
                    Grid.SetColumnSpan(Img, 6);
                    Grid.SetRowSpan(Img, 3);
                    break;

                case Player.state.forward:

                    Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + playerNumber + "/" + imgName + "Forward.png"));
                    Grid.SetColumnSpan(Img, 3);
                    Grid.SetRowSpan(Img, 5);
                    break;

                case Player.state.backward:

                    Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + playerNumber + "/" + imgName + "Backward.png"));
                    Grid.SetColumnSpan(Img, 3);
                    Grid.SetRowSpan(Img, 5);
                    break;
            }
        }

        /// <summary>
        /// changing the player angle compare to his position
        /// </summary>
        /// <param name="currentPosition"></param>
        public void ChangePlayerImageByPosition(int currentPosition)
        {
            if(currentPosition > 0 && currentPosition < 8)
            {
                Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + playerNumber + "/" + imgName +"Forward.png"));
                Grid.SetColumnSpan(Img, 3);
                Grid.SetRowSpan(Img, 5);
            }
            else if (currentPosition > 8 && currentPosition < 16)
            {
                Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + playerNumber + "/" + imgName + "Right.png"));
                Grid.SetColumnSpan(Img, 6);
                Grid.SetRowSpan(Img, 3);
            }
            else if (currentPosition > 16 && currentPosition < 24)
            {
                Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + playerNumber + "/" + imgName + "Backward.png"));
                Grid.SetColumnSpan(Img, 3);
                Grid.SetRowSpan(Img, 5);
            }
            else
            {
                Img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Images/Players/" + playerNumber + "/" + imgName + "Left.png"));
                Grid.SetColumnSpan(Img, 6);
                Grid.SetRowSpan(Img, 3);
            }
            
        }

        /// <summary>
        /// caculate the overall value of the player. Includes: his amount of money,
        /// and his overall buy prive of his property
        /// </summary>
        /// <returns></returns>
        public int CalculatePropertyValue()
        {
            int value = 0;

            foreach (object obj in GameManager.ArrayMap )
            {
                if( obj != null )
                    if ( obj is Property )
                        if (((Property)obj).ownerOfTheProperty == GameManager.currentPlayer)
                            value += ((Property)obj).currentCostToBuy;       
            }
            return value;
        }
        public void AmountOfMoneyChange(int amount)
        {
            amountOfMoney -= amount;
            string formattedNumber = amountOfMoney.ToString("N0"); // adding 
            txtMoney.Text = $"{formattedNumber}";
        }
    }  
}
