using System;
using Arkanoid.GameObjects;
using DataBase.Models;
using GameEngine.GameServices;

namespace Arkanoid.GameServices
{
    public class GameManager : Manager
    {
        public static User User { get; set; } = new User();
        
        public GameManager(Scene scene) : base(scene)
        {
            Init();
        }
        public void Init()
        {
            Scene.RemoveAllObjects();
            int pos = 11;
            int row = 3;
            Random rnd = new Random();
            if (User.Level == null)
                User.Level = new Level();

            int numTypeJelly = 0;
            for (int z =0; z< User.Level.CountYellowRows; z++)
            {
                var currentColorEnum = (Jelly.JellyType)(numTypeJelly);
                for (int i = 1; i < 16; i++)
                {
                    var jelly = new Jelly(Scene, currentColorEnum, 65, pos, row);
                    Scene.AddObject(jelly);
                    pos += 68;
                }
                row += 68;
                pos = 11;
                numTypeJelly++;

                if (numTypeJelly == 3)
                    numTypeJelly = 0;
            }

            var bar = new Bar(Scene, "Images/Bar.png",width:300, lenght: 50,placeX:374,placeY:430,speed:3);
            Scene.AddObject(bar);

            Ball ball = new Ball(Scene, "Images/Ball.png",placeX:494,placeY:350,speed:1,length:60,width:60);
            Scene.AddObject(ball);
        }
    }
}
