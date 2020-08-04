using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeCase.StudyCases
{
    class ToDictionaryFromIEnumerable : IStudy
    {
        public void Execute()
        {
            IEnumerable<string> stringListWithRepeteadElements = new List<string> {"aa", "bb", "aa"};

            var dictionary2 = stringListWithRepeteadElements.Distinct().ToDictionary(x => x, x => x + ".value");

            var dictionary = stringListWithRepeteadElements.ToDictionary(x => x, x => x + ".value"); // exception

            ;
        }
    }
}
