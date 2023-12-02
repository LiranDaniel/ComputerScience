using Arkanoid.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Globalization.DateTimeFormatting;

namespace Arkanoid.GameServices
{
    public class GameManager : Manager
    {
        private int x = 0;
        private int y = 100;

        public GameManager(Scene scene) : base(scene)
        {
            Init();
        }
        public void Init()
        {
            Scene.RemoveAllObjects();
            while(y != 700)
            {
                Jelly jelly = new Jelly(Scene, Jelly.JellyType.yellow, 100, x, y);
                Scene.AddObject(jelly);
                if (x != 1300)
                {
                    x += 100;
                }
                if (x == 1300)
                {
                    y += 200;
                    x = 0;
                }
            }
        }
    }
}
