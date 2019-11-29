using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class ParsingDoubleWithInvariantCulture : IStudy
    {
        public void Execute()
        {
            var firstDouble = "2,2";
            var secondDouble = "2.2";

            var result1 = Convert.ToDouble(firstDouble); //2.2
            var result2 = Convert.ToDouble(secondDouble); //22
            var result3 = Double.Parse(firstDouble, CultureInfo.InvariantCulture); //22
            var result4 = Double.Parse(secondDouble, CultureInfo.InvariantCulture); //2.2

            var result5 = Double.TryParse(firstDouble, out double resultDouble1); //2.2
            var result6 = Double.TryParse(secondDouble, out double resultDouble2); //22

            var result7 = Double.TryParse(firstDouble, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture,out double resultDouble3); //0
            var result8 = Double.TryParse(secondDouble, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double resultDouble4); //2.2

            var result9 = Double.TryParse(firstDouble, NumberStyles.Any, CultureInfo.InvariantCulture, out double resultDouble5); //22
            var result10 = Double.TryParse(secondDouble, NumberStyles.Any, CultureInfo.InvariantCulture, out double resultDouble6); //2.2

            result9 = Double.TryParse(firstDouble.Replace('.', ','), NumberStyles.Any, CultureInfo.InvariantCulture, out double resultDouble7); //22
            result10 = Double.TryParse(secondDouble.Replace('.', ','), NumberStyles.Any, CultureInfo.InvariantCulture, out double resultDouble8); //2.2


            ;
        }
    }
}
