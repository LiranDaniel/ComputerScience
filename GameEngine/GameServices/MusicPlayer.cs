using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.System;

namespace GameEngine.GameServices
{
    public static class MusicPlayer
    {
        private static MediaPlayer _mediaPlayer = new MediaPlayer();
        public static bool IsOn { get; set; } = true;
        public static double Volume { get; private set; } = 70;

        public static void LoadMusicPlayer(string fileName)
        {
            _mediaPlayer.Volume = Volume;
            _mediaPlayer.AutoPlay = true;
            _mediaPlayer.IsLoopingEnabled = true;
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/App/Music/{fileName}")); // OGBackground.wav
        }
        public static void Play()
        {
            IsOn = true;
            _mediaPlayer.Play();
        }
        public static void Stop()
        {
            IsOn = false;
            _mediaPlayer.Pause();
        }
        public static void ChangeVolume(double volume)
        {
            Volume = volume;
            _mediaPlayer.Volume = volume / 100;
        }
    }
}
