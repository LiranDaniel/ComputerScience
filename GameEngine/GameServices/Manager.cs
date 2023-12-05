using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml;
using static GameEngine.GameServices.Constants;

namespace GameEngine.GameServices
{
    public abstract class Manager
    {
        public Scene Scene { get; private set; }
        private static DispatcherTimer _runTimer;                           // הטיימר יפעל ללא הפסקה

        public static GameEvent GameEvent { get; } = new GameEvent();       // יצירת אירוע חדש ממחלקה GameEvent
        private static DispatcherTimer _clockTimer;
        public static GameState GameState = GameState.Loaded;

        public Manager(Scene scene)
        {
            Scene = scene;

            if(_runTimer == null)
            {
                _runTimer = new DispatcherTimer();
                _runTimer.Interval = TimeSpan.FromMilliseconds(Constants.RunInterval);
                _runTimer.Tick += _runTimer_Tick;
                _runTimer.Start();
            }
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if(GameEvent.OnKeyUp != null)
            {
                GameEvent.OnKeyUp(args.VirtualKey);
            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (GameEvent.OnKeyDown != null)
            {
                GameEvent.OnKeyDown(args.VirtualKey);
            }
        }



        /// <summary>
        /// the function is execute in frequency that never stop and turning an event that named OnRun
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _runTimer_Tick(object sender, object e)
        {
            if(GameEvent.OnRun != null)
            {
                GameEvent.OnRun();
            }
        }

        public void Start()
        {
            Scene.Init();
            _runTimer.Start();
            GameState = GameState.Started;
        }

        public void Pause()
        {
            _runTimer.Stop();
            GameState = GameState.Paused;
        }
    }
}
