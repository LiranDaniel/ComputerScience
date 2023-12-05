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
                Stop();
                _X = _toX;
                _Y = _toY;
            }
            base.Render();
        }

        public void Stop()
        {
            _vX = _vY = _vvX = 0;
        }

        /// <summary>
        /// The function is moving the object
        /// </summary>
        /// <param name="toX">horizontal destination</param>
        /// <param name="toY">vertical desination</param>
        /// <param name="speed">velocity</param>
        /// <param name="acceleration">accelaration</param>
        public void MoveTo(double toX, double toY, double speed = 1, double acceleration = 0)
        {
            _toX = toX;
            _toY = toY;

            var len = Math.Sqrt(Math.Pow(_toX - _X, 2) + Math.Pow(_toY - _Y, 2));
            var cos = (_toX - _X) / len;
            var sin = (_toY - _Y) / len;

            speed *= Constants.SpeedUnit;
            _vX = speed * cos;
            _vY = speed * sin;
            _vvX = acceleration * cos;
            _vvY = acceleration * sin;
        }
    }
}
