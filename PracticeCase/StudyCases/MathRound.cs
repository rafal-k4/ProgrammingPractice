using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class MathRound : IStudy
    {
        public void Execute()
        {
            var res1 = Math.Round(4.5); // 4
            var res2 = Math.Round(4.49); // 4
            var res3 = Math.Round(4.51); // 5
            var res4 = Math.Round(0.49); // 0
            var res5 = Math.Round(0.5); // 0
            var res6 = Math.Round(0.51); // 1
            var res7 = Math.Round(4.5, MidpointRounding.AwayFromZero); // 5
            var res8 = Math.Round(4.49, MidpointRounding.AwayFromZero); // 4
            var res9 = Math.Round(4.51, MidpointRounding.AwayFromZero); // 5
            var res10 = Math.Round(0.49, MidpointRounding.AwayFromZero); // 0
            var res11 = Math.Round(0.5, MidpointRounding.AwayFromZero); // 1
            var res12 = Math.Round(0.51, MidpointRounding.AwayFromZero); // 1
            var res13 = Math.Round(5.5, MidpointRounding.AwayFromZero); // 6
            ;
        }
    }
}
