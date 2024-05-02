﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using static BussinesTourProject.Classes.Player;

namespace BussinesTourProject.Classes
{
    public static class GameManager
    {
        public static Random rnd = new Random();

        public static Player currentPlayer;
        public static int currentTimesPlay = 1;
        private static int IndexPlayers = rnd.Next(0, 4);
        public static Player[] arrayPlayers;

        public enum HouseState
        {
            HomeLevel1, HomeLevel2, HomeLevel3, Hotel
        }
        public static object[] ArrayMap = { new House(14_000, 50_000)};

        

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
            arrayPlayers  = new Player[] {new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.right, state.backward, state.left}),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.right, state.backward, state.left} ),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.forward, state.right, state.backward} ),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.forward, state.right, state.backward} )};
        }
        
        public static void Land()
        {
            
           
            if (ArrayMap[currentPlayer.currentPosition] == null)
            {

            }
            else if (ArrayMap[currentPlayer.currentPosition] is House)
            {

            }
            else if (ArrayMap[currentPlayer.currentPosition] is Station)
            {

            }
            else if (ArrayMap[currentPlayer.currentPosition] is Chance) {
                TakeCardChance();
            }
        }
        public static void TakeCardChance()
        {

        }
        public static void LandingHouse()
        {
            if (ArrayMap[currentPlayer.currentPosition] is House ) {
                House LandHouse = (House)ArrayMap[currentPlayer.currentPosition];
                if (LandHouse.playerOwnerHouse == null)
                {
                    //Show the interface to buy house
                }
                else
                {
                    if(LandHouse.playerOwnerHouse == currentPlayer)
                    {
                        //Show Upgrade InterFace
                    }
                    else
                    {
                        if(LandHouse.currentValue > currentPlayer.AmountOfMoney)
                        {
                            if (LandHouse.currentValue > (currentPlayer.CalculatePropertyValue() + currentPlayer.AmountOfMoney))
                            {

                            }
                        }
                        else
                        {
                            currentPlayer.AmountOfMoney -= LandHouse.currentValue;
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
            IndexPlayers++;
            if (IndexPlayers > 3)
                IndexPlayers = 0;
            currentPlayer = arrayPlayers[IndexPlayers];
            currentTimesPlay = 1;
        }
        public static DataBase.Models.User User { get; set; }
    }
}
