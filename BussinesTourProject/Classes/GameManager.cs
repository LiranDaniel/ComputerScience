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

        public static Random rnd = new Random();
        public static Grid UIBuyingHouseGrid { get; set; }
        public static Grid UIJailOptions {  get; set; }
        public static Grid UIBuyingStation{  get; set; }
        public static Image ImgBuyingStation { get; set; }
        public static RadioButton[] arrayRadioButtonBuyingHouse { get; set; } = new RadioButton[3];
        public static TextBlock txtBlockBuyingHousePrice;

        public static Player currentPlayer;
        public static int currentTimesPlay = 1;
        private static int IndexPlayers = rnd.Next(0, 4);
        public static Player[] arrayPlayers;
        public static bool IsDouble;

        public static object[] ArrayMap = {

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
                                                         150, 132, 116, 100, 84, 68, 52, 36} };
        public static int[,] MatrixPositionPlayer2 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 7, 7, 7, 7, 7, 7, 7,
                                                         6, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 82, 82, 82, 82, 82, 82, 82},
                                                       { 12, 12, 12, 12, 12, 12, 12, 12,
                                                         8, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 161, 161, 161, 161, 161, 161, 161,
                                                         160, 139, 123, 107, 91, 75, 59, 43} };
        public static int[,] MatrixPositionPlayer3 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         7, 11, 11, 11, 11, 11, 11, 11,
                                                         0, 22, 30, 38, 46, 54, 62, 70,
                                                         87, 78, 78, 78, 78, 78, 78, 78},
                                                       { 16, 16, 16, 16, 16, 16, 16, 16,
                                                         2, 43, 59, 75, 91, 107, 123, 139,
                                                         160, 165, 165, 165, 165, 165, 165, 165,
                                                         175, 132, 116, 100, 84, 68, 52, 36} };
        public static int[,] MatrixPositionPlayer4 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         14, 11, 11, 11, 11, 11, 11, 11,
                                                         0, 22, 30, 38, 46, 54, 62, 70,
                                                         78, 78, 78, 78, 78, 78, 78, 78},
                                                       { 20, 20, 20, 20, 20, 20, 20, 20,
                                                         2, 36, 52, 68, 84, 100, 116, 132,
                                                         150, 169, 169, 169, 169, 169, 169, 169,
                                                         175, 139, 123, 107, 91, 75, 59, 43} };

        public static double WorldChampionTimes = 1.5;


        public static void IncreaseWorldChampion()
        {
            WorldChampionTimes += .5;
        }
        
        public static void InitPlayers()
        {
            arrayPlayers  = 
            new Player[] {new Player(playerState: new state[4]{state.forward, state.right,
            state.backward, state.left}, name:"Alexander Yanai", imgName:"RedCar" , playerNumber:"Player1"),

            new Player(playerState: new state[4]{state.forward, state.right, state.backward,
            state.left}, name:"Daniel Shlomo", imgName:"RedCar" , playerNumber:"Player1"),

            new Player(playerState: new state[4]{state.forward, state.forward, state.right,
            state.backward}, name:"Liran Daniel", imgName:"RedCar" , playerNumber:"Player1"),

            new Player(playerState: new state[4]{state.forward, state.forward, state.right,
                state.backward}, name:"Oleg woller", imgName:"RedCar" , playerNumber:"Player1")};
        }
        
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
                CheckIfDouble();
            else
                CheckIfDouble();
        }
        public static void TakeCardChance()
        {
        }

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
                    }
                    else
                    {   
                        currentPlayer.amountOfMoney -= LandHouse.currentCostToPayRent;
                        string formattedNumber = currentPlayer.amountOfMoney.ToString("N0"); // adding 
                        currentPlayer.txtMoney.Text = $"{formattedNumber}"; 
                        
                    }
                }
                CheckIfDouble();
            }
        }

        public static void CheckIfDouble()
        {
            timerPlayers.Stop();
            if (!GameManager.IsDouble)
                GameManager.NextPlayer();
            IsDouble = false;
        }

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
                            currentPlayer.amountOfMoney -= LandStation.currentCostToPayRent;
                        }
                    }
                    CheckIfDouble();
                }
            }
        }

        public static void ShowUIJail()
        {
            UIJailOptions.Visibility = Visibility.Visible;
            Jail.txtRemaningRoundJail.Text = $"Remaning Rounds In jail:{currentPlayer.turnsStackJail}";
        }
        public static void WorldChampionShip()
        {

        }
        public static void WorldTour()
        {

        }
        public static int[] RollDice()
        {
            int[] Result = { rnd.Next(1, 7), rnd.Next(1, 7) };
            return Result;
        }
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
