using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    class ClosureFun : IStudy
    {
        public void Execute()
        {
            var arr = new[] { 1, 2, 3, 4, 5 };

            var actions = new List<Action>();

            for (int i = 0; i < arr.Length; i++)
            {
                actions.Add(() => Console.WriteLine($"value of array element is: {i}"));
            }

            actions.ForEach(x => x());
            // ----RESULT----- 
            // value of array element is: 5
            // value of array element is: 5
            // value of array element is: 5
            // value of array element is: 5
            // value of array element is: 5

            actions = new List<Action>();

            for (int i = 0; i < arr.Length; i++)
            {
                var tempVariableForNonClosure = i;
                actions.Add(() => Console.WriteLine($"value of array element is: {tempVariableForNonClosure}"));
            }

            actions.ForEach(x => x());
            // ----RESULT----- 
            // value of array element is: 0
            // value of array element is: 1
            // value of array element is: 2
            // value of array element is: 3
            // value of array element is: 4

            ;
        }
    }
}
