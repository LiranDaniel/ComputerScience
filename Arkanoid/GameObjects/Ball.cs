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
    public class Ball : GameMovingObject
    {
        public double vX => _vX;
        public double _speed;

        public Ball(Scene scene, string filename, double placeX, double placeY, double speed,
            double length, double width) :
            base(scene, filename, placeX, placeY)
        {
            _speed = speed;
            image.Width = width;
            image.Height = width;
            SetImage(filename);
        }
        public override void Render()
        {
            base.Render();
            if (_X <= 0)
            {
                _X = 0;
            }
            else if (_X >= _scene?.ActualWidth - width)
            {
                _X = _scene.ActualWidth - width;
            }
        }
    }
}
