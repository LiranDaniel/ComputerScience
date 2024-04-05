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
        public static Player Player1 = new Player(name: "MainPlayer");
        public static Player Player2 = new Player(name: "Player2");
        public static Player Player3 = new Player(name: "Player3");
        public static Player Player4 = new Player(name: "Player4");
     
        public static int currentDiceResult = 4;

        public static Player currentPlayer;
        public static int currentTimesPlay = 1;
        private static int IndexPlayers = rnd.Next(0, 4);
        public static Player[] arrayPlayers = { Player1, Player2, Player3, Player4 };


        public static void NextPlayer()
        {
            IndexPlayers++;
            if (IndexPlayers > 4)
                IndexPlayers = 0;
            currentPlayer = arrayPlayers[IndexPlayers];
            currentTimesPlay = 1;
        }
    }
}
