using Arkanoid.GameObjects;
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
            var jelly = new Jelly(Scene, Jelly.JellyType.yellow, 100, 1, 1);
            Scene.AddObject(jelly);
            jelly = new Jelly(Scene, Jelly.JellyType.pink, 100, 100, 1);
            Scene.AddObject(jelly);
            jelly = new Jelly(Scene, Jelly.JellyType.green, 100, 200, 1);
            Scene.AddObject(jelly);

            //var bar = new Bar(Scene, "file", speed: 3, width: 160, Scene.ActualWidth / 2 - 80, Scene.Ground);
            //Scene.AddObject(bar);
        }
    }
}
