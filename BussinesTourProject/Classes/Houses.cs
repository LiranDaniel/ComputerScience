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
            BasicHouse = 1,
            AdvanceHouse = 2,
            villa = 3, 
            Hotel = 4
        }
        public static Dictionary<Level, string> filePathImages;// = new Dictionary<Level, Image>();
        public Houses()
        {
            filePathImages = new Dictionary<Level, string>() { 
                {Level.BasicHouse, @"/Assets\Images\SquareImages\House1.png"},
                { Level.AdvanceHouse, @"/Assets\Images\SquareImages\House2.png"},
                {Level.villa, @"/Assets\Images\SquareImages\House3.png"},
                {Level.Hotel, @"/Assets\Images\SquareImages\House4.png" } };
        }
        public Image HouseImage;
        public int position;
    }
}
