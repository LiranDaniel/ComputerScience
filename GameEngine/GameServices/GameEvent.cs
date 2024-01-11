using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace GameEngine.GameServices
{
    public class GameEvent
    {
        public Action OnRun;                        // זהו אירוע אשר אחראי על התנועה
        public Action OnClock;                      // זהו אירוע אשר אחראי להפעיל את השעון של העיקוב
        public Action<VirtualKey> OnKeyUp;          // האירוע אשר אחראי לעזיבת המקש
        public Action<VirtualKey> OnKeyDown;        // האירוע אשר אחראי לחיצת המקש
        public Action<int> OnRemoveHeart;
        public Action OnUpdateScore; 
    }
}
