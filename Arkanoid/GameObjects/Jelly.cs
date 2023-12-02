﻿using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Arkanoid.GameObjects
{
    public class Jelly : GameObject
    {
        public enum JellyType
        {
            green = 0,
            pink = 1,
            yellow = 2,
        }
        private JellyType _jellyType;

        public Jelly(Scene scene, JellyType jellyType, double width, double placeX, double placeY) :
            base(scene, string.Empty, placeX, placeY)
        {
            image.Width = width;
            image.Height = height;
            _jellyType = jellyType;
            SetImage();
        }
        private void SetImage() 
        {
            switch(_jellyType)
            {
                case JellyType.green:
                    base.SetImage("Jelly/Jelly_green.png");
                    break;
                case JellyType.pink:
                    base.SetImage("Jelly/Jelly_pink.png");
                    break;
                case JellyType.yellow:
                    base.SetImage("Jelly/Jelly_yellow.png");
                    break;

            }
        }
    }
}
