using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    class SwapVariables_without_third_helperVariable : IStudy
    {
        public void Execute()
        {
            int x = 2;
            int y = 5;

            Console.WriteLine($"{x} : {y}");

            x ^= y;
            y ^= x;
            x ^= y;

            Console.WriteLine($"{x} : {y}");
        }
    }
}
