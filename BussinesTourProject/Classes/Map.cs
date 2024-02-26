using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BussinesTourProject.Classes
{
    public class Map
    {
        public enum Houses
        {
            HomeLevel1, HomeLevel2, HomeLevel3, Hotel
        }
        public Houses[] HousesPosition;

        public Map()
        {
            
        }
    }
}
