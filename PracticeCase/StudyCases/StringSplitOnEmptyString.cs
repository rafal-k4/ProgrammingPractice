using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PracticeCase.StudyCases
{
    class StringSplitOnEmptyString : IStudy
    {
        public void Execute()
        {
            var defaultStringValue = default(string);

            var splittedString = defaultStringValue?.Split(",", StringSplitOptions.RemoveEmptyEntries);

            // Debug.Assert(splittedString != null, nameof(splittedString) + " != null");

            //cannot do foreach on NULL!
            //foreach (var item in splittedString)
            //{
            //    Console.WriteLine(item);
            //}

            var variable = string.Empty;

            var splittedString2 = variable.Split(",", StringSplitOptions.RemoveEmptyEntries); //return empty array


            foreach (var item in splittedString2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
