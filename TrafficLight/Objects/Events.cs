using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLight.Objects
{
    public class Events
    {
        public static Action<Traffic.TrafficLightsState> OnChangeStateLights;//אירוע שיתרחש כאשר רמזור ישנה את מצבו

    }
}
