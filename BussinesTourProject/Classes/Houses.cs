using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BussinesTourProject.Classes
{
    public class Houses
    {
        public enum Level
        {
            Ground = 0,
            House = 1,
            villa = 2, 
            Hotel = 3
        }
        public static Dictionary<Level, string> filePathImages = new Dictionary<Level, string>();
        public Image HouseImage;
        public int position;
    }
}
