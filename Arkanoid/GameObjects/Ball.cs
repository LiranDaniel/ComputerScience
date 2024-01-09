using Arkanoid.GameObjects;
using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;

namespace Arkanoid.GameObjects
{
    public class Ball : GameMovingObject
    {
        public double vX => _vX;
        public double _speed;
        public int countLife;

        public Ball(Scene scene, string filename, double placeX, double placeY, double speed,
            double length, double width) :
            base(scene, filename, placeX, placeY)
        {
            countLife = 3;
            _vX = 0;
            _vY = 0;
            _speed = speed;
            image.Width = width;
            image.Height = width;
            SetImage(filename);
            Manager.GameEvent.OnKeyDown += KeyDown;
            Manager.GameEvent.OnKeyUp += KeyUp;
        }

        public void KeyUp(VirtualKey key)
        {
            if (_vY == 0)
                _vX = 0;
            
        }

        public void KeyDown(VirtualKey key)
        {
            if (_vY == 0)
            {
                switch (key)
                {
                    case VirtualKey.Left:
                    case VirtualKey.A:
                        MoveTo(int.MinValue, _Y, _vX, 0);
                        break;

                    case VirtualKey.Right:
                    case VirtualKey.D:
                        MoveTo(int.MaxValue, _Y, _vX, 0);
                        break;

                    case VirtualKey.Up:
                    case VirtualKey.W:
                        _vY = -6;
                        break;
                }
            }

        }

        public override void Render()
        {
            if (_X <= 0)
            {
                _vX = -_vX;
                _X = 0;
            }
            else if (_X >= _scene?.ActualWidth - width)
            {
                _vX = -_vX;
                _X = _scene.ActualWidth - width;

            }
            else if (_Y <= 0) // נוגעה בתקרה
            {
                _vY = -_vY;
                _Y = 0;
            }
            else if (_Y >= _scene?.ActualHeight - height)// נגיעת כדור ברצפה פספוס
            {
                Stop();
                _scene.Init();
                if (Manager.GameEvent.OnRemoveHeart != null)
                {
                    Manager.GameEvent.OnRemoveHeart(--countLife);
                }

            }
            base.Render();
        }

        public override void Collide(GameObject gameObject)
        {
            if (gameObject is Bar bar)
            {
                var rect = RectHelper.Intersect(this.Rect, bar.Rect);
                _vY = -_vY;
                if (Math.Abs(bar.vX) != 0)
                {
                    _vX += bar.vX / 2.6; //מעניק לכדור כמחצית מהמהירות של המחבט, כלומר, הכדור נוטה לכיוון תנועת המחבט
                }
                _Y = bar.Rect.Top - height;
            }

            if (gameObject is Jelly jelly)
            {
                var intersectRect = RectHelper.Intersect(Rect, jelly.Rect);
                if (intersectRect.Height > intersectRect.Width) //touch vertical sides
                {
                    _X -= _vX;
                    _vX = -_vX;
                }
                else
                {
                    _Y -= _vY;
                    _vY = -_vY;
                }
                jelly.ChangeJelly();
            }
        }
    }
}