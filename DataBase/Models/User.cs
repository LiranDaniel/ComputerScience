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
        public int Id { get; set; }
        public string Name { get; set; } = "Anonymous";
        public string Email { get; set; }
        public string Password { get; set; }
        public int Money { get; set; } = 100;
        public string CurrentSkin { get; set; } = "Defulte";
        public string CurrentAvatar { get; set; } = "Defulte";



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
        public string GetSkinPath(string skinName){
            return filePathSkins[skinName];
        }
        public string GetAvatarPath(string avatarName) {  return filePathAvatars[avatarName]; }        
    }
}
