using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.System;

namespace Arkanoid.GameObjects
{
    public class Bar : GameMovingObject
    {
        public double vX => _vX;
        double _speed;
        public Bar(Scene scene, string filename,double width,double lenght, double placeX, double placeY, double speed) : 
            base(scene, filename, placeX, placeY)
        {
            _speed = speed;
            image.Width = width;
            image.Height = width;
            SetImage(filename);

            Manager.GameEvent.OnKeyDown += KeyDown;
            Manager.GameEvent.OnKeyUp += KeyUp;
        }
        private void KeyDown(VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.Left:
                    MoveTo(int.MinValue, _Y, _speed, 0);
                    break;
                case VirtualKey.Right:
                    MoveTo(int.MaxValue, _Y, _speed, 0);
                    break; 
                case VirtualKey.A:
                    MoveTo(int.MinValue, _Y, _speed, 0);
                    break;
                case VirtualKey.D:
                    MoveTo(int.MaxValue, _Y, _speed, 0);
                    break;
            }
        }
        private void KeyUp(VirtualKey key)
        {
            switch (key)
            {
                case VirtualKey.Left:
                case VirtualKey.A:
                case VirtualKey.D:
                case VirtualKey.Right:
                    Stop();
                    break;
            }
        }
        public override void Render() 
        {
            base.Render();
            if (_X <= 0)
            {
                _X = 0;
                Stop();
            }
            else if(_X >= _scene?.ActualWidth - width)
            {
                _X = _scene.ActualWidth - width;
                Stop();
            }

        }
    }
}
