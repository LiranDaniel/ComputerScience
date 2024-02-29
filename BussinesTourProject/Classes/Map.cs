using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Xaml;

namespace BussinesTourProject.Classes
{
    public static class Map
    {
        public static Random rnd = new Random();
        public static DispatcherTimer Timer { get; set; }
        public static Player player1;
        public static Player player2;
        public static Player player3;
        public static Player player4;


        public enum Houses
        {
            HomeLevel1, HomeLevel2, HomeLevel3, Hotel
        }
        public static Houses[] HousesPosition;

        public static int[] RollDice()
        {
            int[] Result = { rnd.Next(1, 7), rnd.Next(1, 7) };
            return Result;
        }
    }
}
