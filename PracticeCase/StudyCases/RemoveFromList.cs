using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    class RemoveFromList : IStudy
    {
        public void Execute()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            list.RemoveRange(0, 3); // 4, 5
            ;
        }
    }
}
