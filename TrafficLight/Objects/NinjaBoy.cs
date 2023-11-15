using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace TrafficLight.Objects
{
    public class NinjaBoy : Character
    {
        public NinjaBoy(Image image) : base(image) { }

        protected override void MatchGifToState()
        {
            switch (_state)
            {
                case CharacterStateType.Standing:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/App/Characters/Boy_Idle_gif.gif");              //new Uri("ms-appx:///Assets/GameAssets/gifs/TempleGirl/TempleGirlIdle.gif");
                    break;
                case CharacterStateType.Ready:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/App/Characters/Boy_Jump_gif.gif");
                    break;
                case CharacterStateType.Runing:
                    _bitmapimage.UriSource = new Uri("ms-appx:///Assets/App/Characters/Boy_Run_gif.gif");
                    break;
            }
        }
    }


}
