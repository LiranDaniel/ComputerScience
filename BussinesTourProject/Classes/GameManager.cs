using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using static BussinesTourProject.Classes.Player;

namespace BussinesTourProject.Classes
{
    public static class GameManager
    {
        public static Random rnd = new Random();
        public static Grid UIBuyingHouseGrid { get; set; }
        public static int currentTimesPlay = 1;
        private static int IndexPlayers = rnd.Next(0, 4);
        public static Player[] arrayPlayers;
        public static Player currentPlayer = arrayPlayers[IndexPlayers];

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
            state.backward, state.left}, name:"Alex", imgName: "Player1"),

            new Player(playerState: new state[4]{state.forward, state.right, state.backward,
            state.left}, name:"Daniel", imgName: "Player1"),

            new Player(playerState: new state[4]{state.forward, state.forward, state.right,
            state.backward}, name:"Liran", imgName: "Player1"),

            new Player(playerState: new state[4]{state.forward, state.forward, state.right,
                state.backward}, name:"Oleg", imgName: "Player1")};
        }
        
        public static void Land()
        {
            if (ArrayMap[currentPlayer.currentPosition] == null)
            {
               
            }
            else if (ArrayMap[currentPlayer.currentPosition] is House)
            {
                LandingHouse();
            }
            else if (ArrayMap[currentPlayer.currentPosition] is Station)
            {
            }
            else if (ArrayMap[currentPlayer.currentPosition] is Chance) {
            }
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
                    ShowUIBuyingHouse();  
                else
                    Console.WriteLine(  ); //Show you dont have enough money Ui
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
            }     
        } 
        
        private static void ShowUIBuyingHouse()
        {
            UIBuyingHouseGrid.Visibility = Visibility.Visible;

        }

        public static void LandingStation()
        {
            if (ArrayMap[currentPlayer.currentPosition] is House ) 
            {
                House LandHouse = (House)ArrayMap[currentPlayer.currentPosition];
                if (LandHouse.ownerOfTheProperty == null)
                {
                    //Show the interface to buy house
                }
                else // owned by some player
                {
                    if(LandHouse.ownerOfTheProperty == currentPlayer) // if the ownder of the house is the current player that plays
                    {
                        //Show Upgrade InterFace House
                    }
                    else // he is not the owner which means that he have to pay the rent
                    {
                        if(LandHouse.currentCostToPayRent > currentPlayer.amountOfMoney)
                        {
                            if (LandHouse.currentCostToPayRent > (currentPlayer.CalculatePropertyValue() + currentPlayer.amountOfMoney))
                            {

                            }
                        }
                        else
                        {
                            currentPlayer.amountOfMoney -= LandHouse.currentCostToPayRent;
                        }
                    }
                }
            }
        }

        public static void Jail()
        {

        }
        public static void WorldChampionShip()
        {

        }
        public static void WorldTour()
        {

        }
        public static void CheckOwner(Player player)
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
        }
        public static DataBase.Models.User User { get; set; }
    }
}
