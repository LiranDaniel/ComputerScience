using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BussinesTourProject.Services
{
    public class GameManager
    {

        public DispatcherTimer _timer;
        public static Events Events = new Events();

        public GameManager()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
        }

        public void Start()
        {
            _timer.Start();
        }

        private void _timer_Tick(object sender, object e)
        {
            if(Events.OnMovingPlayer!=null)
            {
                Events.OnMovingPlayer(2);
            }
        }
    }
}
