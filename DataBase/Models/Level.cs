using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace DataBase.Models
{
    public class Level
    {
        public int Id { get; set; }
        public int LevelNumber { get; set; }
        public double BallSpeed { get; set; }
        public int BarWith {  get; set; }
        public int CountYellow { get; set; }
        public int CountGreen { get; set; }
        public int CountPink { get; set; }

        public Level()
        {
            
        }
    }
}
