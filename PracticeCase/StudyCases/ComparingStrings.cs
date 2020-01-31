using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class ComparingStrings : IStudy
    {
        public void Execute()
        {
            var string1 = "aaAA";
            var string2 = "AAaa";

            var result = string.Equals(string1, string2); //false
            var result2 = string.Equals(string1, string2, StringComparison.OrdinalIgnoreCase); //true
            var result3 = string.Equals(string1, string2, StringComparison.InvariantCultureIgnoreCase); //true



            var string3 = " aaaa";
            var string4 = "AAaa";

            var resul4 = string.Equals(string3, string4); //false
            var result5 = string.Equals(string3, string4, StringComparison.OrdinalIgnoreCase); //false
            var result6 = string.Equals(string3, string4, StringComparison.InvariantCultureIgnoreCase); //false
            var result7 = string.Equals(string3, string4, StringComparison.Ordinal); //false

            ;

            var string5 = "aaAA";
            var string6 = "AAaa";

            var result8 = string.Compare(string5, string6); //-1
            var result9 = string.Compare(string5, string6, true); //0

            ;

            var string7 = "aaa";
            var string8 = "aaa";

            var result10 = string.Compare(string7, string8); //0
            var result11 = string.Compare(string7, string8, true); //0

            ;

            var string9 = "abc";
            var string10 = "fgh";

            var result12 = string.Compare(string9, string10); //-1
            var result13 = string.Compare(string9, string10, true); //-1

            ;
        }
    }
}
