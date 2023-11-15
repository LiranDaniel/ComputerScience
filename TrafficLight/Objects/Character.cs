using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.EnterpriseData;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TrafficLight.Objects
{
    public class Character
    {
        public Image _image;
        protected BitmapImage _bitmapimage;
        protected CharacterStateType _state;


        public enum CharacterStateType
        {
            Standing, Ready, Runing
        }

        public Character(Image image)
        {
            _image = image;
            _bitmapimage = new BitmapImage();
            _image.Source = _bitmapimage;
            MatchGifToState();

            Events.OnChangeStateLights += SetState;
        }

        public void SetState(Traffic.TrafficLightsState state)//מחליף מצב דמות לפי מצב רמזור
        {
            switch (state)
            {
                case Traffic.TrafficLightsState.Green:
                    _state = CharacterStateType.Runing;
                    break;
                case Traffic.TrafficLightsState.Yellow:
                    _state = CharacterStateType.Ready;
                    break;
                case Traffic.TrafficLightsState.Red:
                    _state = CharacterStateType.Standing;
                    break;


            }
            MatchGifToState();
        }

        protected virtual void MatchGifToState() { }           //פעולה שדורסים אותה כדי לשנות את התוכן שלה בכל אובייקט
    }
}
