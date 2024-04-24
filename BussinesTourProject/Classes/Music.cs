﻿using System;
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
        public static MediaPlayer _mediaPlayer = new MediaPlayer();
        public static bool IsOn { get; set; } = true;
        public static double Volume { get; private set; } = 0;

        public static void LoadMusicPlayer(string fileName)
        {
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Music/{fileName}")); // OGBackground.wav
            _mediaPlayer.Volume = 1;

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
