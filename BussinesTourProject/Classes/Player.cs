﻿using System;
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
        public string playerNumber;
        public Image Img;
        public state[] playerState;
        public int turnsStackJail;
        public TextBlock txtMoney;
        public TextBlock txtState;
        public int playerStations = 0;

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
