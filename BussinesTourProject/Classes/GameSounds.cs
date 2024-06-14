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
        public static bool IsOn = false;                            // IsOn variable that present of the music is active or not 

        /// <summary>
        /// Set the music file and start it 
        /// </summary>
        /// <param name="filename"></param>
        public static void SetState()
        {
            if (IsOn)
            {
                GameManager.SoundPlayer.Volume = 1;
            }
            else
                GameManager.SoundPlayer.Volume = 0;
        }

        /// <summary>
        /// stop the music mediaplayer
        /// </summary>
    }
}
