﻿using Arkanoid.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;

namespace Arkanoid.GameServices
{
    public class GameManager : Manager
    {
        public GameManager(Scene scene) : base(scene)
        {
            Init();
        }
        public void Init()
        {
            Scene.RemoveAllObjects();
            int pos = 3;
            int row = 3;
            
            Random rnd = new Random();
            for (int z =1; z<4; z++)
            {
                var currentColorEnum = (Jelly.JellyType)rnd.Next(0, 3);
                for (int i = 1; i < 11; i++)
                {
                    var jelly = new Jelly(Scene, currentColorEnum, 100, pos, row);
                    Scene.AddObject(jelly);
                    pos += 103;
                }
                row += 103;
                pos = 0;
            }

            //var bar = new Bar(Scene, "file", speed: 3, width: 160, Scene.ActualWidth / 2 - 80, Scene.Ground);
            //Scene.AddObject(bar);
        }
    }
}
