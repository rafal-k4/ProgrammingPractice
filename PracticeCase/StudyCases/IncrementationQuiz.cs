using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class IncrementationQuiz : IStudy
    {
        public void Execute()
        {
            int a = 2;
            int a2 = 2;
            int a3 = 2;

            var b = a++ + ++a; //6
            var b2 = ++a2 + ++a2; //7
            var b3 = a3++ + a3++; //5
            ;
        }
    }
}
