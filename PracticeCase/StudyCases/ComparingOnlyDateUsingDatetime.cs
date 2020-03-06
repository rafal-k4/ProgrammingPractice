using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class ComparingOnlyDateUsingDatetime : IStudy
    {
        public void Execute()
        {
            var date1 = DateTime.Now;
            var date2 = DateTime.Now.AddMinutes(10);

            var compare1 = date1 == date2; // false
            var compare2 = date1.Date == date2.Date; // true

            ;
        }
    }
}
