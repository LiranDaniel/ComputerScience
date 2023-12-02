using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameServices
{
    public abstract class Manager
    {
        public Scene Scene { get; private set; }

        public Manager(Scene scene)
        {
            Scene = scene;
        }
    }
}
