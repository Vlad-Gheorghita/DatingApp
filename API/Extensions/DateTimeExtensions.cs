using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob)  //Chestia asta cu "this DateTime dob" adauga o extensie/"metoda" la clasa DateTime care se numeste CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if(dob.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}