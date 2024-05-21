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

namespace BussinesTourProject.Classes
{
    public static class GameManager
    {
        public static DispatcherTimer timerPlayers;
        public static bool ToggleState { get; set; } = true;

        public static Random rnd = new Random();
        public static Grid UIBuyingHouseGrid { get; set; } // The Grid of buying house in game page
        public static Grid UIJailOptions {  get; set; }    // The Grid of Jail Options in game page
        public static Grid UIBuyingStation{  get; set; }   // The Grid of buying Station in game page
        public static Grid UITax {  get; set; }            // The Grid of paying the taxes of player propertys
        public static Image ImgBuyingStation { get; set; } // The image that display when using the UI station, chaning it to the current Station
        public static RadioButton[] arrayRadioButtonBuyingHouse { get; set; } = new RadioButton[3]; // radio buttons from the Grid buying house from Game page
        public static TextBlock txtBlockBuyingHousePrice; // the text that present the price of the house  that you want to buy
        public static TextBlock txtBlockTaxesPrice;

        public static Player currentPlayer; // current player playing 
        public static int currentTimesPlay = 1; // how much rounds in row did he play to check if he needs  to go to jail
        private static int IndexPlayers = rnd.Next(0, 4); // Select randomly the first player to play, and points to player in the arrayOfPayers
        public static Player[] arrayPlayers; // array of all the players
        public static bool IsDouble; // Check if double

        public static object[] ArrayMap = {
             /// <summary>
             /// This is the array of all the components of the map, include propety, chane, Tax and other special square
             /// </summary>

            null, new House(basicCostToBuy:60_000, basicCostToPayRent:15_000, levelUpgradeRent:35_000, levelUpgradePrice:50_000),
            new House(60_000, 16_000, 35_000, 50_000), new House(60_000, 17_000, 35_000, 50_000), new Station(),
            new House(70_000, 50_000, 35_000, 50_000), new House(70_000, 50_000, 35_000, 50_000),
            new House(70_000, 50_000, 35_000, 50_000), // first Line

            null, new House(90_000, 100_000, 35_000, 100_000), new House(90_000, 100_000, 35_000, 100_000),
            new House(90_000, 100_000, 35_000, 100_000), new Chance(), new House(100_000, 100_000, 35_000, 100_000),
            new Station(), new House(105_000, 100_000, 35_000, 100_000), // Second line

            null, new House(90_000, 100_000, 35_000, 150_000), new Station(),
            new House(90_000, 100_000, 35_000, 150_000), new Chance(), new House(90_000, 100_000, 35_000, 150_000),
            new House(90_000, 100_000, 35_000, 150_000), new House(90_000, 100_000, 35_000, 150_000), // Third line

            null, new Station(), new House(90_000, 100_000, 35_000, 200_000), new House(90_000, 100_000, 35_000, 200_000),
            new Chance(), new House(90_000, 100_000, 35_000, 200_000), new Tax(), new House(90_000, 100_000, 35_000, 200_000) //fourth line

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

        public static double WorldChampionTimes = 1.5; // Time world champion ships times its currentretPrice of the house


        /// <summary>
        /// Increase the amount of times that the world championships increase the price of a house
        /// </summary>
        public static void IncreaseWorldChampion()
        {
            WorldChampionTimes += .5;
        }

        /// <summary>
        /// Initiate the players data
        /// </summary>
        public static void InitPlayers()
        {
            arrayPlayers = 
            new Player[] {new Player(playerState: new state[4]{state.forward, state.right,
            state.backward, state.left}, name:"Alexander Yanai", imgName:"TrackCar" , playerNumber:"Player1"),

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
             if (ArrayMap[currentPlayer.currentPosition] == null)
                 CheckIfDouble();
             else if (ArrayMap[currentPlayer.currentPosition] is House)
                 LandingHouse();
             else if (ArrayMap[currentPlayer.currentPosition] is Station)
                 LandingStation();
             else if (ArrayMap[currentPlayer.currentPosition] is Chance)
                 CheckIfDouble();
             else if (ArrayMap[currentPlayer.currentPosition] is Jail)
                 ShowUIJail();
             else if (ArrayMap[currentPlayer.currentPosition] is Tax)
                 ShowUITax();
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

                        }
                        else
                            Console.WriteLine(); // BunkRupt Giving all the money to the owner and losing the game
                    }
                    else
                    {   
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
                                //Show sell HouseOptions
                            }
                            else
                                Console.WriteLine(); // BunkRupt Giving all the money to the owner and losing the game
                        }
                        else
                        {
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
            {
                currentPlayer.amountOfMoney -= taxPropertysValue;
                currentPlayer.txtMoney.Text = currentPlayer.amountOfMoney.ToString("N0");
            }
        }

        public static void WorldChampionShip()
        {

        }

        public static void WorldTour()
        {

        }

        public static void ShowUISellProperty()
        {

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

            if (currentPlayer.turnsStackJail != 0)
                ShowUIJail();
        }
        public static DataBase.Models.User User { get; set; }
    }
}
