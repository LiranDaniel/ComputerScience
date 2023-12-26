﻿using Arkanoid.GameObjects;
using GameEngine.GameObjects;
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
            
            for (int z =0; z<3; z++)
            {
                var currentColorEnum = (Jelly.JellyType)(z);
                for (int i = 1; i < 11; i++)
                {
                    var jelly = new Jelly(Scene, currentColorEnum, 100, pos, row);
                    Scene.AddObject(jelly);
                    pos += 103;
                }
                row += 103;
                pos = 3;
            }

            var bar = new Bar(Scene, "Images/Bar.png",width:300, lenght: 200,placeX:374,placeY:330,speed:2);
            Scene.AddObject(bar);

            Ball ball = new Ball(Scene, "Images/Ball.png",placeX:300,placeY:400,speed:1,length:60,width:60);
            Scene.AddObject(ball);

        }
    }
}
