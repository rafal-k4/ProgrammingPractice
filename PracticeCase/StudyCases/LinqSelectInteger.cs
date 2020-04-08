using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class LinqSelectInteger : IStudy
    {
        public void Execute()
        {
            var collection = new List<TempModel>
            {
                new TempModel
                {
                    Name = "aa",
                    Index = 1,
                    NullableIndex = 1
                },
                new TempModel
                {
                    Name = "bb",
                    Index = 2,
                    NullableIndex = 2
                }

            };

            var result1 = collection.Where(x => x.Index > 3).ToList(); // empty list
            var result2 = collection.Where(x => x.Index > 3).Select(x => x.Index).ToList(); // empty list

            var result3 = collection.Where(x => x.Index > 3).Select(x => x.Index)
                .FirstOrDefault(); // 0
            var result3_NullableInt = collection.Where(x => x.NullableIndex > 3).Select(x => x.NullableIndex)
                .FirstOrDefault(); // null

            var result5 = collection.Where(x => x.Index > 3).Select(x => x.Index)
                ?.FirstOrDefault(); // <- there is null check, result = 0
            var result5_NullableInt = collection.Where(x => x.NullableIndex > 3).Select(x => x.NullableIndex)
                ?.FirstOrDefault(); // <- there is null check, result = null

            var result6 = collection.Where(x => x.Index > 3).Select(x => x.Index)
                ?.FirstOrDefault() ?? 1; // 0
            var result6_NullableInt = collection.Where(x => x.NullableIndex > 3).Select(x => x.NullableIndex)
                ?.FirstOrDefault() ?? 1; // 1

            // var result4 = collection.Where(x => x.Index > 3).Select(x => x.Index).First(); // exception thrown
            ;
        }
    }


    class TempModel
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public int? NullableIndex { get; set; }
    }
}
