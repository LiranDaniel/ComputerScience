using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace BussinesTourProject.Classes
{
    public static class Music
    {
        public static MediaPlayer _mediaPlayer = new MediaPlayer(); // media player object, the object that will play the music
        public static bool IsOn { get; set; } = true;               // IsOn variable that present of the music is active or not
        public static double Volume { get; private set; } = 100;    // volume of the music

        /// <summary>
        /// Load the music: its file and its volume and everthing
        /// </summary>
        /// <param name="fileName"></param>
        public static void LoadMusicPlayer(string fileName)
        {
            _mediaPlayer.Volume = Volume;
            _mediaPlayer.AutoPlay = true;
            _mediaPlayer.IsLoopingEnabled = true;
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Music/{fileName}")); 

        }
        /// <summary>
        /// plays the music
        /// </summary>
        public static void Play()
        {
            IsOn = true;
            _mediaPlayer.Play();
        }

        /// <summary>
        /// stop the music
        /// </summary>
        public static void Stop()
        {
            IsOn = false;
            _mediaPlayer.Pause();
        }

        /// <summary>
        /// Changing the music volume
        /// </summary>
        /// <param name="volume"></param>
        public static void ChangeVolume(double volume)
        {
            Volume = volume;
            _mediaPlayer.Volume = volume / 100;
        }
    }
}
