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
                    case VirtualKey.Up:
                        _vY = -6;
                        break;
                    case VirtualKey.Right:
                        MoveTo(int.MaxValue, _Y, _speed, 0);
                        break;
                    case VirtualKey.Left:
                        MoveTo(int.MinValue, _Y, _speed, 0);
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
                /*if (Manager.GameEvent.OnRemoveHeart != null)
                {
                    Manager.GameEvent.OnRemoveHeart(--_countLife);
                }*/

            }
            base.Render();
        }
    }
}













/*


namespace Arcanoid1.GameObjects
{
    public class Ball : GameMovingObject
    {

        //public double dX => _dX;
        //public double dY => _dY;
        public double _speed;
        private int _countLife;





        /// <summary>
        ///
        /// </summary>
        /// <param name="scene">במת המשחק</param>
        /// <param name="fileName">שם קובץ שמתאר את המחבא</param>
        /// <param name="speed">מהירות אופקית</param>
        /// <param name="width">מהירות אנכית</param>
        /// <param name="placeX">מיקום יווצרות הכדור ביחס לציר האופקי</param>
        /// <param name="placeY">מיקום יווצרות המחבט ביחס לציר אנכי</param>
        public Ball(Scene scene, string fileName, double speed, int width, double placeX, double placeY) :
            base(scene, fileName, placeX, placeY)
        {
            _dX = 0;
            _dY = 0;
            _speed = speed;
            _countLife = 3;
            Image.Height = width; // כך אנו קובעים את עובי הכדור
            Image.Width = width; // קובעים את הרוחב הכדור
            //Image.Stretch = Windows.UI.Xaml.Media.Stretch.Fill; // כך אנו מותחים את מראה המחבט
            Manager.GameEvent.OnKeyDown += KeyDown;
            Manager.GameEvent.OnKeyUp += KeyUp;
        }


        public override void Collide(GameObject gameObject) // CheckCollistion פעולה מתבצעת רק כאשר יש נגיעה וודאית שבודקים את זה בפונקציה
        {
            if (gameObject is Bar bar)
            {
                _dY = -_dY;
                if (Math.Abs(bar.dX) > 0)
                {
                    _dX += bar.dX / 2.6; // מעניק לכדור כמחצית המהירות של המחבט, כלומר. הכדור נוטה לכיוון תנועת המחבט
                }
                _Y = bar.Rect.Top - Height;
            }
            if (gameObject is Jelly jelly)// בדיקת פגיעה וגם מוצאת את גיילי זה
            {
                var intersectRect = RectHelper.Intersect(Rect, jelly.Rect); // בדיקת התנגשות בין כדור לגיילי
                if (intersectRect.Height > intersectRect.Width) // בדיקת פגיעה שאחרי זה לאן הכדור ילך
                {
                    _X -= _dX;
                    _dX = -_dX;
                }
                else
                {
                    _Y -= _dY;
                    _dY = -_dY;
                }
                jelly.ChangeJelly();
            }


        }

        public override void Render()// מתבצעת כל הזמן
        {
            // בדיקת קצוות הזירה
            if (_X <= 0) // נגיעה בקיר שמאלי
            {
                _dX = -_dX;
                _X = 0;
            }
            else if (_X >= _scene?.ActualWidth - Width) // כאשר תיגע עם הצד הימני שלך בקיר הימני של הזירה
            {
                _dX = -_dX;
                _X = _scene.ActualWidth - Width; // עוצר לך בפינה

            }
            else if (_Y <= 0) // נוגעה בתקרה
            {
                _dY = -_dY;
                _Y = 0;
            }
            else if (_Y >= _scene?.ActualHeight - Height)// נגיעת כדור ברצפה פספוס
            {
                Stop();
                _scene.Init();
                if (Manager.GameEvent.OnRemoveHeart != null)
                {
                    Manager.GameEvent.OnRemoveHeart(--_countLife);
                }

            }
            //else if (_Y >= _scene?.ActualHeight - Height) // בדיקת רצפה
            //{
            //    _dY = -_dY;
            //    _Y = _scene.ActualHeight - Height;
            //}
            base.Render();
        }

        //public override void Render()
        //{
        //    if (_X >= _scene?.ActualWidth - Width || _X <= 0)
        //    {
        //        _dX *= -1;
        //    }
        //    else if (_Y >= _scene?.ActualHeight - Height || _Y <= 0)
        //    {
        //        _dY *= -1;
        //    }
        //    else if (_Y >= _scene?.ActualHeight - Height)
        //    {
        //    }

        //    base.Render();
        //}

        //Messenger.Send(Place);
    }

}

*/
