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
        public static Dictionary<int, int> dictionary = new Dictionary<int, int>();
        public static int IncorrectAnswer = 0;

        public static void Init()
        {
            dictionary.Clear();
            IncorrectAnswer = 0;
        }
        public static void AddAnswer(int numberOfQuetion, int answer)
        {
            dictionary.Add(numberOfQuetion, answer);
        }
        public static void ChangeAnnser(int numberOfQuetion, int answer)
        {
            dictionary[numberOfQuetion] = answer;
        }      
    }
}
