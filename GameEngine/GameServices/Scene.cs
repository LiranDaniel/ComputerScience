using GameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
