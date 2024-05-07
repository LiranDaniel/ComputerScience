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
        /// <summary>
        /// Check if the string is includes some digit
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsIncludeDigit(string str)
        {
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the mail is valid
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static bool IsMailValid(string mail)
        {
            if((mail.IndexOf("@") < 0) || (mail.IndexOf(".") < mail.IndexOf("@")) ||
                (mail.IndexOf(".") == mail.Length -1) ||
                (mail.Length < 12) || !IsIncludeDigit(mail))
                return false;

            return true;
        }

        /// <summary>
        /// Check if the password is valid
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsPasswordValid(string password) 
        {
            if ((password.Length < 8) || (password.ToUpper() == password) ||
                (password.ToLower() == password) || !IsIncludeDigit(password))
                return false;

            return true;
        }
    }
}
