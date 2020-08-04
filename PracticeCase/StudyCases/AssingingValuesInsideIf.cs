using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    class AssingingValuesInsideIf : IStudy
    {
        public void Execute()
        {
            bool booleanVariable = true;

            if (booleanVariable = false)
            {
                Console.WriteLine("Hello from IF");
            }
            else
            {
                Console.WriteLine("Hello from ELSE");
            }

            bool booleanVariable2 = false;

            if (booleanVariable2 = true)
            {
                Console.WriteLine("Hello from IF");
            }
            else
            {
                Console.WriteLine("Hello from ELSE");
            }

            Console.ReadKey();

        }
    }
}
