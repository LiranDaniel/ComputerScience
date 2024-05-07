using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace BussinesTourProject.Classes
{
    public static class GameSounds
    {
        public static MediaPlayer _mediaPlayer = new MediaPlayer(); // object that holds will be control on the game sounds effects
        public static bool IsOn = false;                            // IsOn variable that present of the music is active or not 

        /// <summary>
        /// Set the music file and start it 
        /// </summary>
        /// <param name="filename"></param>
        public static void Play(string filename)
        {
            if (IsOn)
            {
                _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Sounds/{filename}"));
                _mediaPlayer.Play();
            }
        }

        /// <summary>
        /// stop the music mediaplayer
        /// </summary>
        public static void Stop()
        {
            _mediaPlayer.Pause();
        }
    }
}
