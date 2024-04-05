using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesTourProject.Classes
{
    public static class GameManager
    {
        public static Random rnd = new Random();

        public static Player currentPlayer;
        public static int currentTimesPlay = 1;
        private static int IndexPlayers = rnd.Next(0, 4);
        public static Player[] arrayPlayers;

        public static int[,] MatrixPositionPlayer1 = { { 79, 69, 61, 53, 45, 37, 29, 21,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 8, 8, 8, 8, 8, 8, 8, 8,
                                                         20, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 169, 169, 169, 169, 169, 169, 169,
                                                         148, 132, 116, 100, 84, 68, 52, 36} };

        public static int[,] MatrixPositionPlayer2 = { { 79, 69, 61, 53, 45, 37, 29, 21,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 8, 8, 8, 8, 8, 8, 8, 8,
                                                         20, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 169, 169, 169, 169, 169, 169, 169,
                                                         148, 132, 116, 100, 84, 68, 52, 36} };

        public static int[,] MatrixPositionPlayer3 = { { 79, 69, 61, 53, 45, 37, 29, 21,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 8, 8, 8, 8, 8, 8, 8, 8,
                                                         20, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 169, 169, 169, 169, 169, 169, 169,
                                                         148, 132, 116, 100, 84, 68, 52, 36} };

        public static int[,] MatrixPositionPlayer4 = { { 79, 69, 61, 53, 45, 37, 29, 21,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 8, 8, 8, 8, 8, 8, 8, 8,
                                                         20, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 169, 169, 169, 169, 169, 169, 169,
                                                         148, 132, 116, 100, 84, 68, 52, 36} };


        public static void InitPlayers()
        {
            arrayPlayers  = new Player[] {new Player(imgName: "Player1"),
            new Player(imgName: "Player1"),
            new Player(imgName: "Player1"),
            new Player(imgName: "Player1")};
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
