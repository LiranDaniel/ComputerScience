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
        public int LevelNumber { get; set; } = 1;
        public double BallSpeed { get; set; } = 1;
        public int BarWith { get; set; } = 300;
        public int BarLenght { get; set; } = 50;
        public int CountYellowRows { get; set; } = 1;
        public int CountGreenRows { get; set; } = 1;
        public int CountPinkRows { get; set; } = 1;

        public void Init()
        {
            this.LevelNumber = 1;
            this.BallSpeed = 1;
            this.BarWith = 300;
            this.BarLenght = 80;
            this.CountYellowRows = 1;
            this.CountGreenRows = 1;
            this.CountPinkRows = 1;

        }

    }
}
