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
        public GameManager(Scene scene) : base(scene)
        {
            Init();
        }
        public void Init()
        {
            Scene.RemoveAllObjects();
            var jelly = new Jelly(Scene, Jelly.JellyType.yellow, 200, 200, 300);
            Scene.AddObject(jelly); 
        }
    }
}
