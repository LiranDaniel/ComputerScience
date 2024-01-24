using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace EnglishProject.Classes
{
    public static class Answer
    {
        public static int IncorrectAnswer;

        public static void Init()
        {
            IncorrectAnswer = 0;
        }
        
        public static void InCorrectAnswer()
        {
            IncorrectAnswer++;
        }
        public static bool IsLost()
        {
            return IncorrectAnswer >= 3;
        }
    }
}
