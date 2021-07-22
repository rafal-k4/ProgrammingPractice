using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    class ActivatorCreateInstanceStudyCase: IStudy
    {
        public void Execute()
        {
            var stringResult = GetInstance<string>(); //new String does not contain parameterless construtor
                                                      //(so in GetInstance there is a special case of string.Empty.ToCharArray() as a constructor parameter
            var refType = GetInstance<SomeReferenceType>(); // instance of ref type (properties with default values, int = 0, ref = null)
            var dateTimeType = GetInstance<DateTime>(); // default dateTime 01/01/0001
            var structType = GetInstance<double>(); // default struct value

            Console.ReadLine();
        }

        private T GetInstance<T>()
        {
            return typeof(T) == typeof(string)
                ? (T) Activator.CreateInstance(typeof(T), new object[] { string.Empty.ToCharArray() })
                : Activator.CreateInstance<T>();
        }


    }

    class SomeReferenceType
    {
        public int someInteger { get; set; }
        public SomeNestedRefType nestedRefType { get; set; }
    }

    class SomeNestedRefType
    {
        public int someNestedInt { get; set; }
    }
}
