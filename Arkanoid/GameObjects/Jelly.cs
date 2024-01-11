using Arkanoid.GameServices;
using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Arkanoid.GameObjects
{
    public class Jelly : GameObject
    {
        public enum JellyType
        {
            green = 0,
            pink = 1,
            yellow = 2,
        }
        private JellyType _jellyType;
        public override Rect Rect => new Rect(_X, _Y, width, height);

        public Jelly(Scene scene, JellyType jellyType, double width, double placeX, double placeY) :
            base(scene, string.Empty, placeX, placeY)
        {
            image.Width = width;
            image.Height = width; 
            _jellyType = jellyType;
            SetImage();
        }
        private void SetImage() 
        {
            switch(_jellyType)
            {
                case JellyType.green: 
                    base.SetImage("Images/GameImages/jelly_green.png");
                    break;
                case JellyType.pink:
                    base.SetImage("Images/GameImages/jelly_pink.png");
                    break;
                case JellyType.yellow:
                    base.SetImage("Images/GameImages/jelly_yellow.png");
                    break;

            }
        }
        public void ChangeJelly()
        {
            switch(_jellyType)
            {
                case JellyType.green:
                    _scene.RemoveObject(this);
                    GameManager.User.Score++;
                    if (Manager.GameEvent.OnUpdateScore != null)
                        Manager.GameEvent.OnUpdateScore();
                    break;
                case JellyType.pink:
                    _jellyType = JellyType.green;
                    SetImage();
                    break;
                case JellyType.yellow:
                    _jellyType = JellyType.pink;
                    SetImage();
                    break;
            }
        }
    }
}
