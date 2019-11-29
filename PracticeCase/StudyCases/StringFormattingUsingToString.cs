using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    internal class StringFormattingUsingToString : IStudy
    {
        public void Execute()
        {
            var number = 20.55123;

            var formatted = number.ToString("0000");
            var formatted2 = $"{number:##.###}";
            var formatted3 = $"{number:###.###}";
            var formatted4 = $"{number:#####.########}";

            Console.WriteLine(formatted);
            Console.WriteLine(formatted2);
            Console.WriteLine(formatted3);
            Console.WriteLine(formatted4);
            ;
        }
    }
}
