using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class StringJoinStudy : IStudy
    {
        public void Execute()
        {
            var listOfSomething = new List<string>();
            var listOfSomething2 = new List<string> { "aa", "bb", "cc " };

            var joinedString = string.Join(",", listOfSomething);
            var joinedString2 = string.Join(",", listOfSomething2);

            //result: when array is empty returned values is empty string

        }
    }
}
