using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameObjects
{
    /// <summary>
    /// The class is a part of base for any object that can move
    /// </summary>
    public abstract class GameMovingObject : GameObject
    {
        protected GameMovingObject(Scene scene, string filename, double placeX, double placeY) :
        base(scene, filename, placeX, placeY) { }
    }
}
