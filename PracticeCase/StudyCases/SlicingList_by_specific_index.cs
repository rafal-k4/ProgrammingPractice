using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    class SlicingList_by_specific_index : IStudy
    {
        public void Execute()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

            var sliceIndex = 3;

            // expected: one list of { 1, 2, 3} and other of { 4, 5, 6, 7 }

            var firstSlicedList = list.GetRange(0, sliceIndex); // 1, 2, 3
            var secondSlicedList = list.GetRange(sliceIndex, list.Count - sliceIndex); // 4, 5, 6, 7
            ;

        }
    }
}
