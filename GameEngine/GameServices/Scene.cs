using GameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameEngine.GameServices
{
    public class Scene : Canvas
    {
        private List<GameObject> _gameObjects = new List<GameObject>();
        protected List<GameObject> _gameObjectsSnapshot => _gameObjects.ToList();
        public double Ground;

        public Scene() 
        {
            Manager.GameEvent.OnRun += Run;
            Manager.GameEvent.OnRun += CheckCollision; //בזכות שורה זאת הפעולה פועלת ללא הפסקה
        }

        public void CheckCollision() //הפעולה פועלת ללא ערב כתגובה לאירוע OnRun
        {
            foreach (var gameObject in _gameObjectsSnapshot) //עוברים על כל רשימת האובייקטים
            {
                if (gameObject.Collisional) //אם האובייקט לא שקוף
                {
                    //מחפשים מופע ראשון של אובייקט, אשר הוא לא אותו האובייקט, הוא לא שקוף והוא נגע באובייקט הנוכחי
                    var otherObject = _gameObjectsSnapshot.FirstOrDefault(g =>
                    !ReferenceEquals(g, gameObject) &&
                    g.Collisional
                    && !RectHelper.Intersect(g.Rect, gameObject.Rect).IsEmpty);
                    if (otherObject != null)
                    {
                        //כל אובייקט רושם מחדש את הפעולה מפני שמגיב אחרת, כלומר אם הפעולה נקראת זה אומר שבוודאות קרתה התנגשות.
                        // כדי שיכולו להגיב באופן מיוחד Collide כל אובייקט ידרוס את הפעולה
                        gameObject.Collide(otherObject);
                    }
                }
            }
        }

        private void Run()
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject is GameMovingObject)
                {
                    gameObject.Render();
                }
            }
        }

        public void Init()
        {
            foreach (GameObject gameObject in _gameObjects) 
            {
                gameObject.Init();            
            }
        }
        public void RemoveAllObjects()
        {
            foreach(GameObject gameObject in _gameObjects)
            {
                RemoveObject(gameObject);
            }
        }
        public void RemoveObject(GameObject gameObject)
        {
            if (_gameObjects.Contains(gameObject))
            {
                _gameObjects.Remove(gameObject);
                Children.Remove(gameObject.image);
            }
        }
        public void AddObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            Children.Add(gameObject.image);
        }

    }
}