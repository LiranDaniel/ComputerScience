using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;

namespace DataBase.DataBaserManager
{
    public class CheckValidation
    {       
        private static bool IsIncludeDigit(string str)
        {
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    return true;
            }
            return false;
        }

        public static bool  IsMailValid(string mail)
        {
            if((mail.IndexOf("@") == -1) || (mail.IndexOf(".") < mail.IndexOf("@")) ||
                (mail.IndexOf("@") != 0) || (mail.IndexOf(".") < mail.Length) ||
                (mail.Length > 12) || (IsIncludeDigit(mail)))
                return false;

            return true;
        }

        public static bool IsPasswordValid(string password) 
        {
            if ((password.Length < 8) || (password.ToUpper() == password) ||
                (password.ToLower() == password) || (IsIncludeDigit(password)))
                return false;

            return true;
        }
    }
}
