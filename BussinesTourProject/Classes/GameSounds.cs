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
        public static MediaPlayer _mediaPlayer = new MediaPlayer();

        public static bool IsOn = false;

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
