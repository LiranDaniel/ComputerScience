using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Arkanoid.GameObjects
{
    public class Bar : GameMovingObject
    {
        public Bar(Scene scene, string filename, double placeX, double placeY) : base(scene, filename, placeX, placeY)
        {

                
        }
        private void KeyDown(VirtualKey key)
        {

        }
    }
}
