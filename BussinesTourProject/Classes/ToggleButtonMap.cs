using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace BussinesTourProject.Classes
{
    public class ToggleButtonMap
    {
        public int Position { get; set; }
        public ToggleButton button { get; set; }

        public static ToggleButtonMap[] ArrayToggleButtonMap;

        public ToggleButtonMap(int position, ToggleButton button)
        {
            this.Position = position;
            this.button = button;
        }

    }
}
