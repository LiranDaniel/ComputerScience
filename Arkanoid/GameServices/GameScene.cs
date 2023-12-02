using Arkanoid.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.GameServices
{
    public class GameScene : Scene
    {
        public int JellyBrickCount => _gameObjectsSnapshot.Count(x => x is Jelly);
    }
}
