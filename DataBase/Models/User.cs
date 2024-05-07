using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace DataBase.Models
{
    public class User
    {
        public int Id { get; set; } // id of the player in the database
        public string Name { get; set; } = "Anonymous"; // his name in the database
        public string Email { get; set; } // his email in the database
        public int Money { get; set; } = 100; // his amount of money in the database
        public string CurrentSkin { get; set; } = "Defulte"; // his current sking in the database
        public string CurrentAvatar { get; set; } = "Defulte"; // his current avatar in the database

        private static Dictionary<string, string> filePathSkins = new Dictionary<string, string>() {
                {"Defulte", @"/Assets\Images\SquareImages\House1.png"},
                {"CoolCar1", @"/Assets\Images\SquareImages\House2.png"},
                {"RaceCar", @"/Assets\Images\SquareImages\House3.png"}
        };

        private static Dictionary<string, string> filePathAvatars = new Dictionary<string, string>() {
                {"Defulte", @"/Assets\Images\SquareImages\House1.png"},
                {"CoolCar1", @"/Assets\Images\SquareImages\House2.png"},
                {"RaceCar", @"/Assets\Images\SquareImages\House3.png"}
        };

        public string GetSkinPath(string skinName) {return filePathSkins[skinName];}

        public string GetAvatarPath(string avatarName) {  return filePathAvatars[avatarName]; }        
    }
}
