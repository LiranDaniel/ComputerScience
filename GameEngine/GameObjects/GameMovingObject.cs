using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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

        public override void Render()
        {
            _vX += _vvX;
            _vY += _vvY;

            _X += _vX;
            _Y += _vY;

            if (Math.Abs(_X - _toX) < 4 && Math.Abs(_Y - _toY) < 4)
            {
                
            }
            base.Render();
        }

        public void Stop()
        {
            _vX = _vY = _vvX = 0;
        }
    }
}
