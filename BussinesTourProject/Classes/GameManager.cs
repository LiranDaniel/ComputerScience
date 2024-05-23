using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using static BussinesTourProject.Classes.Player;
using Windows.Perception.Spatial;
using Windows.ApplicationModel.VoiceCommands;
using Windows.UI.Xaml.Media;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace BussinesTourProject.Classes
{
    public static class GameManager
    {
        public static DispatcherTimer timerPlayers;
        public static bool ToggleState { get; set; } = true;

        public static MediaPlayer SoundPlayer { get; set; } = new MediaPlayer();
        public static void PlaySound(string file)
        {
            SoundPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Music/{file}"));
            SoundPlayer.Play();
        }
        public static Random rnd = new Random();
        public static Grid UIBuyingHouseGrid { get; set; } // The Grid of buying house in game page
        public static Grid UIJailOptions {  get; set; }    // The Grid of Jail Options in game page
        public static Grid UIBuyingStation{  get; set; }   // The Grid of buying Station in game page
        public static Grid UITax {  get; set; }            // The Grid of paying the taxes of player propertys
        public static Grid UIWorldChampion { get; set; }   // The Grid of selecting property to World Champion
        public static Grid UIWorldTour { get; set; }       // The Grid of selecting World Tour square
        public static Grid UIMessage { get; set; }         // This Grid will display 
        public static Grid UISellingProperty { get; set; } // This Grid will display an options to sell on of your propertys to pay other rent
        public static Image ImgBuyingStation { get; set; } // The image that display when using the UI station, chaning it to the current Station
        public static RadioButton[] arrayRadioButtonBuyingHouse { get; set; } = new RadioButton[3]; // radio buttons from the Grid buying house from Game page
        public static TextBlock txtBlockBuyingHousePrice;  // The text that present the price of the house  that you want to buy
        public static TextBlock txtBlockTaxesPrice;
        public static TextBlock txtBlockMessage;           // The text will be displayed 

        public static Player currentPlayer; // current player playing 
        public static int currentTimesPlay = 1; // how much rounds in row did he play to check if he needs  to go to jail
        private static int IndexPlayers = rnd.Next(0, 4); // Select randomly the first player to play, and points to player in the arrayOfPayers
        public static Player[] arrayPlayers; // array of all the players
        public static bool IsDouble; // Check if double

        public static object[] ArrayMap = {
             /// <summary>
             /// This is the array of all the components of the map, include propety, chane, Tax and other special square
             /// </summary>

            null, new House(basicCostToBuy:55_000, basicCostToPayRent:2_000, levelUpgradeRent:25_000, levelUpgradePrice:50_000),
            new House(60_000, 2_000, 26_000, 50_000), new House(65_000, 3_000, 27_000, 50_000), new Station(),
            new House(100_000, 5_000, 33_000, 50_000), new House(110_000, 6_000, 34_000, 50_000),
            new House(120_000, 8_000, 35_000, 50_000), // first Line

            null, new House(140_000, 10_000, 64_000, 100_000), new House(150_000, 11_000, 66_000, 100_000),
            new House(160_000, 12_000, 68_000, 100_000), new Chance(), new House(180_000, 14_000, 78_000, 100_000),
            new Station(), new House(200_000, 16_000, 80_000, 100_000), // Second line

            null, new House(220_000, 18_000, 102_000, 150_000), new Station(),
            new House(240_000, 19_000, 104_000, 150_000), new Chance(), new House(260_000, 20_000, 118_000, 150_000),
            new House(270_000, 22_000, 120_000, 150_000), new House(280_000, 24_000, 122_000, 150_000), // Third line

            null, new Station(), new House(300_000, 26_000, 160_000, 200_000), new House(320_000, 28_000, 165_000, 200_000),
            new Chance(), new House(350_000, 35_000, 172_000, 200_000), new Tax(), new House(400_000, 38_000, 178_000, 200_000) //fourth line

        };
        public static int[,] MatrixPositionPlayer1 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 7, 7, 7, 7, 7, 7, 7,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 82, 82, 82, 82, 82, 82, 82},
                                                       { 8, 8, 8, 8, 8, 8, 8, 8,
                                                         20, 43, 59, 75, 91, 107, 123, 139,
                                                         175, 157, 157, 157, 157, 157, 157, 157,
                                                         150, 132, 116, 100, 84, 68, 52, 36} }; // array of all the position that the player1 should be on, the position in the square
        public static int[,] MatrixPositionPlayer2 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 7, 7, 7, 7, 7, 7, 7,
                                                         6, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 82, 82, 82, 82, 82, 82, 82},
                                                       { 12, 12, 12, 12, 12, 12, 12, 12,
                                                         8, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 161, 161, 161, 161, 161, 161, 161,
                                                         160, 139, 123, 107, 91, 75, 59, 43} }; // array of all the position that the player2 should be on, the position in the square
        public static int[,] MatrixPositionPlayer3 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         7, 11, 11, 11, 11, 11, 11, 11,
                                                         0, 22, 30, 38, 46, 54, 62, 70,
                                                         87, 78, 78, 78, 78, 78, 78, 78},
                                                       { 16, 16, 16, 16, 16, 16, 16, 16,
                                                         2, 43, 59, 75, 91, 107, 123, 139,
                                                         160, 165, 165, 165, 165, 165, 165, 165,
                                                         175, 132, 116, 100, 84, 68, 52, 36} }; // array of all the position that the player3 should be on, the position in the square
        public static int[,] MatrixPositionPlayer4 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         14, 11, 11, 11, 11, 11, 11, 11,
                                                         0, 22, 30, 38, 46, 54, 62, 70,
                                                         78, 78, 78, 78, 78, 78, 78, 78},
                                                       { 20, 20, 20, 20, 20, 20, 20, 20,
                                                         2, 36, 52, 68, 84, 100, 116, 132,
                                                         150, 169, 169, 169, 169, 169, 169, 169,
                                                         175, 139, 123, 107, 91, 75, 59, 43} }; // array of all the position that the player4 should be on, the position in the square


        /// <summary>
        /// Initiate the players data
        /// </summary>
        public static void InitPlayers()
        {
            arrayPlayers = 
            new Player[] {new Player(playerState: new state[4]{state.forward, state.right,
            state.backward, state.left}, name:"Alexander Yanai", imgName:"RedCar" , playerNumber:"Player1"),

            new Player(playerState: new state[4]{state.forward, state.right, state.backward,
            state.left}, name:"Daniel Shlomo", imgName:"YellowCar" , playerNumber:"Player2"),

            new Player(playerState: new state[4]{state.forward, state.forward, state.right,
            state.backward}, name:"Liran Daniel", imgName:"PurpleCar" , playerNumber:"Player3"),

            new Player(playerState: new state[4]{state.forward, state.forward, state.right,
                state.backward}, name:"Oleg woller", imgName:"BlueCar" , playerNumber:"Player4")};
        }

        /// <summary>
        /// When landing on a square calling a function that will check which type of square it is than call other function squares
        /// </summary>
        public static void Land()
        {       
             if (ArrayMap[currentPlayer.currentPosition] is House)
                LandingHouse();
             else if (ArrayMap[currentPlayer.currentPosition] is Station)
                LandingStation();
             else if (ArrayMap[currentPlayer.currentPosition] is Chance)
                WorldChampionShip();
             else if (ArrayMap[currentPlayer.currentPosition] is Jail)
                ShowUIJail();
             else if (ArrayMap[currentPlayer.currentPosition] is Tax)
                ShowUITax();
             else if (currentPlayer.currentPosition == 16)
                WorldChampionShip();
             else if (currentPlayer.currentPosition == 24)
                WorldTour();
             else
                CheckIfDouble();
        }

        public static void TakeCardChance()
        {
        }

        /// <summary>
        /// When landing on a house square check if there is a owner. IF there is then the player pays to him.
        /// if there is not owner than show and option to buy the house
        /// </summary>
        public static void LandingHouse()
        {
            House LandHouse = (House)ArrayMap[currentPlayer.currentPosition];
            if (LandHouse.ownerOfTheProperty == null)
            {
                //Show the interface to buy house
                if (LandHouse.basicCostToBuy <= currentPlayer.amountOfMoney)
                {
                    UIBuyingHouseGrid.Visibility = Visibility.Visible;
                    foreach(RadioButton btnRadio in arrayRadioButtonBuyingHouse)
                        btnRadio.IsChecked = false;
                    txtBlockBuyingHousePrice.Text = "Select To Buy";
                }
                else
                    //Show you dont have enough money Ui
                    CheckIfDouble();
            }
            else // owned by some player
            {
                if(LandHouse.ownerOfTheProperty == currentPlayer) // if the ownder of the house is the current player that plays
                {
                    //Show Upgrade InterFace House

                }
                else // he is not the owner which means that he have to pay the rent
                {   
                    if(LandHouse.currentCostToPayRent > currentPlayer.amountOfMoney) // doesnot have enough money
                    {   
                        if (LandHouse.currentCostToPayRent > (currentPlayer.CalculatePropertyValue() + currentPlayer.amountOfMoney))
                        {
                            NotEnoughMoney(LandHouse);
                        }
                        else
                            BankRupt(LandHouse); // BunkRupt Giving all the money to the owner and losing the game
                    }
                    else
                    {
                        PlaySound("Paying.wav");
                        currentPlayer.AmountOfMoneyChange(LandHouse.currentCostToPayRent);
                    }
                }
                CheckIfDouble();
            }
        }

        /// <summary>
        /// If there is doule, if there isn't doube then move the round to the next player, else show him an option to play again
        /// </summary>
        public static void CheckIfDouble()
        {
            timerPlayers.Stop();
            if (!GameManager.IsDouble)
                GameManager.NextPlayer();
            IsDouble = false;
        }

        /// <summary>
        /// When landing on Station check if the Station have an owner, If not then showing an UI for buying that Station
        /// </summary>
        public static void LandingStation()
        {


            if (ArrayMap[currentPlayer.currentPosition] is Station ) 
            {
                Station LandStation = (Station)ArrayMap[currentPlayer.currentPosition];
                if (LandStation.ownerOfTheProperty == null)
                {
                    //Show the interface to buy house
                    if (LandStation.basicCostToBuy <= currentPlayer.amountOfMoney)
                    {
                        UIBuyingStation.Visibility = Visibility.Visible;
                        ImgBuyingStation.Source = LandStation.imageOfProperty.Source;
                    }
                    else
                        CheckIfDouble();
                        // Show Doesnt Have Enough money
                }
                else // owned by some player
                {
                    if(LandStation.ownerOfTheProperty != currentPlayer) // if the ownder of the house is the current player that plays// he is not the owner which means that he have to pay the rent
                    {
                        if(LandStation.currentCostToPayRent > currentPlayer.amountOfMoney)
                        {
                            if (LandStation.currentCostToPayRent > (currentPlayer.CalculatePropertyValue() + currentPlayer.amountOfMoney))
                            {
                                NotEnoughMoney(LandStation);
                                //Show sell HouseOptions
                            }
                            else
                                BankRupt(LandStation); // BunkRupt Giving all the money to the owner and losing the game

                            return;
                        }
                        else
                        {
                            PlaySound("Paying.wav");
                            currentPlayer.AmountOfMoneyChange(LandStation.currentCostToPayRent);
                        }
                    }
                    CheckIfDouble();
                }
            }
        }

        /// <summary>
        /// Show UI Jail and display the remaning rounds inside the jail
        /// </summary>
        public static void ShowUIJail()
        {
            UIJailOptions.Visibility = Visibility.Visible;
            Jail.txtRemaningRoundJail.Text = $"Remaning Rounds In jail:{currentPlayer.turnsStackJail}";
        }

        public async  static void ShowUITax()
        {
            int taxPropertysValue = currentPlayer.CalculatePropertyValue() / 10;
            txtBlockTaxesPrice.Text = $"{taxPropertysValue.ToString("N0")}";
            UITax.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2));

            UITax.Visibility = Visibility.Collapsed;

            if (currentPlayer.amountOfMoney < taxPropertysValue)
            {
                // Show UI To sell House
            }
            else
                currentPlayer.AmountOfMoneyChange(taxPropertysValue);
            
        }

        public async static void WorldChampionShip()
        {
            bool IsThereProperty = false;
            ToggleState = true;
            SetToggleState(false); // first enable only the property buttons
            foreach (object square in ArrayMap)
            {
                if(square is Property)
                {
                    if((square as Property).ownerOfTheProperty == currentPlayer)
                    {
                        (square as Property).toggleButtonBlock.IsEnabled = true;
                        IsThereProperty = true;
                    }
                }
            }
            if (!IsThereProperty)   // if there is not property then the not property UI will be displayed
            {
                //Show UI that the player doesnt have propertys
                txtBlockMessage.Text = "You Does Not Have\n   Any Propertys";
                UIMessage.Visibility = Visibility.Visible;
                await Task.Delay(TimeSpan.FromSeconds(3));
                UIMessage.Visibility = Visibility.Collapsed;
            }
            else
                UIWorldChampion.Visibility = Visibility.Visible;
                // Show UI Select World Champion
            
            // else then the UI to Select world Champion will be displayed

        }

        public static void SetToggleState(bool state)
        {
            foreach (object square in ArrayMap)
            {
                if(square is Property)
                {
                    (square as Property).toggleButtonBlock.IsChecked = false;
                    (square as Property).toggleButtonBlock.IsEnabled = state;
                }
            }
        }

        public async static void WorldTour()
        {
            bool IsThereIsSquare = false;
            ToggleState = true;
            SetToggleState(false); // first enable only the property buttons
            foreach (object square in ArrayMap)
            {
                if (square is Property)
                {
                    if ((square as Property).ownerOfTheProperty == currentPlayer || (square as Property).ownerOfTheProperty == null)
                    {
                        (square as Property).toggleButtonBlock.IsEnabled = true;
                        IsThereIsSquare = true;
                    }
                }
            }
            if (!IsThereIsSquare)   // if there is not property then the not property UI will be displayed
            {
                // Show UI that the player doesnt have propertys
                txtBlockMessage.Text = "Theres Is No Free Square";
                UIMessage.Visibility = Visibility.Visible;
                await Task.Delay(TimeSpan.FromSeconds(3));
                UIMessage.Visibility = Visibility.Collapsed;
            }
            else
                UIWorldTour.Visibility = Visibility.Visible;
                // Show Ui Select World Champion
            


            // first enable only the property buttons and the null buttons
            // after showing ui to go into that position if there is 
        }

        // Return Square Position In ArrayMap
        public static int GetSquarePosition()
        {
            int position = -1;
            bool IsSquareChecked = false;
            foreach (object square in GameManager.ArrayMap)
            {
                if (square is Property)
                {
                    if ((square as Property).toggleButtonBlock.IsChecked == true)
                    {
                        IsSquareChecked = true;
                        break;
                    }
                }
                position++;
            }
            if (IsSquareChecked)
                return position;
            else
                return -1;
        }
        public static int GetSquaresPosition()
        {
            List<int> positions = new List<int>();   
            int position = -1;
            bool IsSquareChecked = false;
            foreach (object square in GameManager.ArrayMap)
            {
                if (square is Property)
                {
                    if ((square as Property).toggleButtonBlock.IsChecked == true)
                    {
                        IsSquareChecked = true;
                        break;
                    }
                }
                position++;
            }
            if (IsSquareChecked)
                return position;
            else
                return -1;
        }

        /// <summary>
        /// return array at size of 2 includes random resulte of the dices
        /// </summary>
        /// <returns></returns>
        public static int[] RollDice()
        {
            int[] Result = { rnd.Next(1, 7), rnd.Next(1, 7) };
            return Result;
        }

        /// <summary>
        /// calling Next player to play
        /// </summary>
        public static void NextPlayer()
        {
            arrayPlayers[IndexPlayers].txtState.Visibility = Visibility.Collapsed;
            IndexPlayers++;
            if (IndexPlayers > 3)
                IndexPlayers = 0;
            currentPlayer = arrayPlayers[IndexPlayers];
            currentPlayer.txtState.Visibility = Visibility.Visible;
            currentTimesPlay = 1;

            if (arrayPlayers[IndexPlayers] == null)
                NextPlayer();

            if (currentPlayer.turnsStackJail != 0)
                ShowUIJail();
        }
        public static DataBase.Models.User User { get; set; }

        public async static void NotEnoughMoney(Property property)
        {
            bool IsThereProperty = false;
            ToggleState = false;
            SetToggleState(false); // first enable only the property buttons
            foreach (object square in ArrayMap)
            {
                if (square is Property)
                {
                    if ((square as Property).ownerOfTheProperty == currentPlayer)
                    {
                        (square as Property).toggleButtonBlock.IsEnabled = true;
                        IsThereProperty = true;
                    }
                }
            }
            if (!IsThereProperty)   // if there is not property then the not property UI will be displayed
            {
                //Show UI that the player doesnt have propertys
                txtBlockMessage.Text = "You Does Not Have\nAny Propertys To Sell";
                UIMessage.Visibility = Visibility.Visible;
                await Task.Delay(TimeSpan.FromSeconds(3));
                UIMessage.Visibility = Visibility.Collapsed;

                BankRupt(property);
            }
            else
                UISellingProperty.Visibility = Visibility.Visible;
        }
        
        public async static void BankRupt(Property property)
        {
            currentPlayer.txtMoney.Text = "BankRupt";
            currentPlayer.txtMoney.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            property.ownerOfTheProperty.amountOfMoney += currentPlayer.CalculatePropertyValue() + currentPlayer.amountOfMoney;

            property.ownerOfTheProperty.txtMoney.Text = $"{property.ownerOfTheProperty.amountOfMoney}";

            foreach (object square in ArrayMap)
            {
                if (square is Property)
                {
                    if ((square as Property).ownerOfTheProperty == currentPlayer)
                    {
                        (square as Property).SellProperty();
                    }
                }
            }
            for (int i = 0; i < arrayPlayers.Length; i++)
            {
                if (arrayPlayers[i] == currentPlayer)
                    arrayPlayers[i] = null;
            }
            txtBlockMessage.Text = "You Doesn't Have Any\nMoney Left";
            UIMessage.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(3));
            UIMessage.Visibility = Visibility.Collapsed;

            NextPlayer();

        }
    }
}
