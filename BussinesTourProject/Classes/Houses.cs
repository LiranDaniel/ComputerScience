﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace BussinesTourProject.Classes
{
    public class Houses
    {
        public enum Level
        {
            None = 0,
            BasicHouse = 1,
            AdvanceHouse = 2,
            villa = 3, 
            Hotel = 4
        }
        public static Dictionary<Level, string> filePathImages = new Dictionary<Level, string>() {
                {Level.BasicHouse, @"/Assets\Images\SquareImages\House1.png"},
                { Level.AdvanceHouse, @"/Assets\Images\SquareImages\House2.png"},
                {Level.villa, @"/Assets\Images\SquareImages\House3.png"},
                {Level.Hotel, @"/Assets\Images\SquareImages\House4.png" } };

        private static int[] arrayTimesValue = { 0, 1, 2, 3, 4 };
        public Image HouseImage;
        private int basicValue;
        private int currentValue;
        public int state;
        public int position;


        public Houses(int basicValue, int position)
        {
            this.basicValue = basicValue;
            this.position = position;
            state = 0;
        }
        public void ChangeHouse(int level)
        {   
            HouseImage.Source = new BitmapImage(new Uri($"ms-appx://" + filePathImages[(Level)level]));
            state = level;
            currentValue = basicValue * arrayTimesValue[level];
        }
    }
}
