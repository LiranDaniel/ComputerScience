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

        public static int[,] MatrixPositionPlayer1 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         14, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 8, 8, 8, 8, 8, 8, 8, 8,
                                                         20, 43, 59, 75, 91, 107, 123, 139,
                                                         175, 157, 157, 157, 157, 157, 157, 157,
                                                         150, 132, 116, 100, 84, 68, 52, 36} };

        public static int[,] MatrixPositionPlayer2 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         0, 8, 8, 8, 8, 8, 8, 8,
                                                         6, 22, 30, 38, 46, 54, 62, 70,
                                                         94, 83, 83, 83, 83, 83, 83, 83},
                                                       { 12, 12, 12, 12, 12, 12, 12, 12,
                                                         8, 36, 52, 68, 84, 100, 116, 132,
                                                         175, 161, 161, 161, 161, 161, 161, 161,
                                                         160, 139, 123, 107, 91, 75, 59, 43} };

        public static int[,] MatrixPositionPlayer3 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         7, 12, 12, 12, 12, 12, 12, 12,
                                                         0, 22, 30, 38, 46, 54, 62, 70,
                                                         87, 79, 79, 79, 79, 79, 79, 79},
                                                       { 16, 16, 16, 16, 16, 16, 16, 16,
                                                         2, 43, 59, 75, 91, 107, 123, 139,
                                                         160, 165, 165, 165, 165, 165, 165, 165,
                                                         175, 132, 116, 100, 84, 68, 52, 36} };

        public static int[,] MatrixPositionPlayer4 = { { 82, 70, 62, 54, 46, 38, 30, 22,
                                                         14, 12, 12, 12, 12, 12, 12, 12,
                                                         0, 22, 30, 38, 46, 54, 62, 70,
                                                         79, 79, 79, 79, 79, 79, 79, 79},
                                                       { 20, 20, 20, 20, 20, 20, 20, 20,
                                                         2, 36, 52, 68, 84, 100, 116, 132,
                                                         150, 169, 169, 169, 169, 169, 169, 169,
                                                         175, 139, 123, 107, 91, 75, 59, 43} };


        public static void InitPlayers()
        {
            arrayPlayers  = new Player[] {new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.right, state.backward, state.left}),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.right, state.backward, state.left} ),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.forward, state.right, state.backward} ),
            new Player(imgName: "Player1", playerState: new state[4]{state.forward, state.forward, state.right, state.backward} )};
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
