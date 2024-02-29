using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BussinesTourProject.Services
{
    public class Manager
    {

        private DispatcherTimer _timer;
        public static Events Events = new Events();

        public Manager()
        {
                _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
            _timer.Tick += _timer_Tick;
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
