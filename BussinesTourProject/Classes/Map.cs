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
        public static DispatcherTimer Timer { get; set; }


        public enum Houses
        {
            HomeLevel1, HomeLevel2, HomeLevel3, Hotel
        }
        public static Houses[] HousesPosition;
    }
}
