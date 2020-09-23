using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace PracticeCase.StudyCases
{
    class LINQ_OrderBy_FirstOrDefault_with_side_effect : IStudy
    {
        public void Execute()
        {
            var numbers = new List<int> { 14, 12, 5, 2, 4, 6, 8 };

            var result1 = numbers.FirstOrDefault(x => TryRegisterUserExample(x));
            Console.WriteLine($"Registered User without order by: {result1}");
            Console.ReadLine();

            #region description
            // In this case, where there is OrderBy before FirstOrDefault, TryRegisterUserExample is called
            // for every number in numbers collection
            #endregion
            var result2 = numbers.OrderBy(x => x).FirstOrDefault(x => TryRegisterUserExample(x));
            Console.WriteLine($"Registered User: {result2}");
            Console.ReadLine();

            #region description
            // In this case, where there is LazyLoadedFirstOrDefault user, TryRegisterUserExample is called only once
            #endregion
            var result3 = numbers.OrderBy(x => x).LazyLoadedFirstOrDefault(x => TryRegisterUserExample(x));
            Console.WriteLine($"Registered User using LazyLoadedFirstOrDefault: {result2}");
            Console.ReadLine();
            ;
        }

        private bool TryRegisterUserExample(int i)
        {
            Console.WriteLine($"Registration of users: {i}");
            return true;
        }

    }
}
