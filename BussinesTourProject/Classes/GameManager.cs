using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static int[,] MatrixPositionPlayer1 = { { 15, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 175, 6, 8, 8, 8, 8, 8, 8,
                                                         8, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 169, 169, 169, 169, 169, 169, 169,
                                                         148, 132, 116, 100, 84, 68, 52, 36} };

        public static int[,] MatrixPositionPlayer2 = { { 10, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 175, 13, 12, 12, 12, 12, 12, 12,
                                                         12, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 169, 169, 169, 169, 169, 169, 169,
                                                         148, 132, 116, 100, 84, 68, 52, 36} };

        public static int[,] MatrixPositionPlayer3 = { { 5, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 175, 20, 16, 16, 16, 16, 16, 16,
                                                         16, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 169, 169, 169, 169, 169, 169, 169,
                                                         148, 132, 116, 100, 84, 68, 52, 36} };

        public static int[,] MatrixPositionPlayer4 = { { 14, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 175, 27, 20, 20, 20, 20, 20, 20,
                                                         20, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 169, 169, 169, 169, 169, 169, 169,
                                                         148, 132, 116, 100, 84, 68, 52, 36} };


        public static void InitPlayers()
        {
            arrayPlayers  = new Player[] {new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.right, state.left, state.backward}),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.right, state.left, state.backward} ),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.right, state.left, state.backward} ),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.right, state.left, state.backward} )};
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
