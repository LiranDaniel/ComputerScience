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
        protected double _vX;       // horizontal velocity
        protected double _vY;       // vertical velocity
        protected double _vvX;      // hotizontal acceleration 
        protected double _vvY;      // vertical acceleration
        protected double _toX;      // destination horizontal
        protected double _toY;      // destination vertical

        protected GameMovingObject(Scene scene, string filename, double placeX, double placeY) :
        base(scene, filename, placeX, placeY) { }
    }
}
