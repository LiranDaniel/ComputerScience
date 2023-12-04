using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GameEngine.GameObjects
{
    public abstract class GameObject
    {
        protected double _X; // Current Place x
        protected double _Y; // Current Place y

        protected double _placeX; // Start Place x
        protected double _placeY; // Start Place y

        public Image image;         // Image it self object
        protected string _fileName; // name of the file 

        public double width => image.ActualWidth;
        public double height => image.ActualHeight;
        public virtual Rect Rect => new Rect(_X, _Y, width, height);

        public bool Collisional = true;

        protected Scene _scene;

        public GameObject(Scene scene, string fileName, double placeX, double placeY)
        {
            _scene = scene; 
            //_fileName = fileName;
            _placeX = placeX;
            _placeY = placeY;
            _X = _placeX;
            _Y = _placeY;
            image = new Image();
            SetImage(fileName);
            Render();
        }
        public virtual void Init()
        {
            _X = _placeX;   
            _Y = _placeY;
        }
        public virtual void Collide(GameObject gameObject)
        {

        }
        public virtual void Render()
        {
            Canvas.SetLeft(image, _X);
            Canvas.SetTop(image, _Y);
        }
        protected void SetImage(string filename)
        {
            image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{filename}"));
        }
    }
}
