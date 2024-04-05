using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        
    }
}
