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
                    _bitmapimage.UriSource = new Uri("");                  //new Uri("ms-appx:///Assets/GameAssets/gifs/TempleGirl/TempleGirlIdle.gif");
                    break;
                case CharacterStateType.Ready:
                    _bitmapimage.UriSource = new Uri("");
                    break;
                case CharacterStateType.Runing:
                    _bitmapimage.UriSource = new Uri("");
                    break;
            }
        }
    }


}
