using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace TrafficLight.Objects
{
    public class Traffic
    {
        private Ellipse _elpRed;
        private Ellipse _elpYellow;
        private Ellipse _elpGreen;
        private bool _isAuto;
        private TrafficLightsState _state;


        private DispatcherTimer _timer;//משתנה שיעביר את הרמזור באופן אוטומטי ממצב אחד לשני

        public bool IsAuto
        {

            get { return _isAuto; }
            set
            {
                _isAuto = value;
                if (_isAuto)
                {
                    _timer.Start();
                }
                else
                    _timer.Stop();

            }
        }

        public enum TrafficLightsState //הגדרת מצבים של הרמזור
        {
            Red, Yellow, Green
        }

        public Traffic(Ellipse elpRed, Ellipse elpYellow, Ellipse elpGreen)
        {
            _elpRed = elpRed;
            _elpYellow = elpYellow;
            _elpGreen = elpGreen;
            _isAuto = false;
            _state = TrafficLightsState.Red; //רמזור ימצא במצב אדום לכשיוצר

            _timer = new DispatcherTimer();                 //כך יוצרים טיימר

            _timer.Interval = TimeSpan.FromMilliseconds(400);      //כך קובעים תדירות פעולת הטיימר
            _timer.Stop();                                  //כאשר הטיימר יוצר הוא אהיה במצב דומם
            _timer.Tick += _timer_Tick;
        }
        private void _timer_Tick(object sender, object e)
        {
            SetState();
        }


        /// <summary>
        /// הפעולה מיועדת להעביר את הרמזור ממצב אחד למצב שני באופן מעגלי
        /// </summary>
        public void SetState()
        {
            Reset();
            switch (_state)
            {
                case TrafficLightsState.Red:
                    _state = TrafficLightsState.Yellow;
                    _elpYellow.Fill = new SolidColorBrush(Colors.Yellow);//כך צובעים אליפסה בצבע
                    break;
                case TrafficLightsState.Yellow:
                    _state = TrafficLightsState.Green;
                    _elpGreen.Fill = new SolidColorBrush(Colors.Green);//כך צובעים אליפסה בצבע
                    break;
                case TrafficLightsState.Green:
                    _state = TrafficLightsState.Red;
                    _elpRed.Fill = new SolidColorBrush(Colors.Red);//כך צובעים אליפסה בצבע
                    break;
            }
            if (Events.OnChangeStateLights != null) //
            {
                Events.OnChangeStateLights(_state);
            } //
        }

        private void Reset()
        {
            _elpRed.Fill = new SolidColorBrush(Colors.Transparent);
            _elpYellow.Fill = new SolidColorBrush(Colors.Transparent);
            _elpGreen.Fill = new SolidColorBrush(Colors.Transparent);
        }

        public class Objects
        {
            public class TrafficLight
            {
            }
        }
    }
}
