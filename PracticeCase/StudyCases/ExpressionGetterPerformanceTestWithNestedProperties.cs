using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace PracticeCase.StudyCases
{
    class ExpressionGetterPerformanceTestWithNestedProperties : IStudy
    {
        class T1
        {
            public string FirstProp { get; set; }
            public int SecondProp { get; set; }
            public T2 nestedProp { get; set; }
        }

        class T2
        {
            public string NestedFirstProp { get; set; }
            public int NestedSecondProp { get; set; }
            public T3 EvenMoreNestedProp { get; set; }
        }

        class T3
        {
            public string SomeString { get; set; }
        }


        public static Func<object, string> GetDelegate(string propertyName)
        {
            return null;

        }

        public void Execute()
        {
            var propertyName = "nestedProp.EvenMoreNestedProp.SomeString";
            var f = CreateFieldGetterA<T1>(propertyName);
            


            PropertyInfo propertyInfo = typeof(T1).GetProperty("FirstProp");

            

            var d = (Func<T1, string>)Delegate.CreateDelegate(typeof(Func<T1, string>), propertyInfo.GetMethod);
            

            T1 testObj = new T1()
            {
                FirstProp = "First Prop VALUE",
                SecondProp = 10,
                nestedProp = new T2
                {
                    NestedFirstProp = "Nested Prop 1",
                    NestedSecondProp = 42,
                    EvenMoreNestedProp = new T3
                    {
                        SomeString = "Even more nested property"
                    }
                }
            };


            PropertyInfo propInfo1 = testObj.GetType().GetProperty("nestedProp");
            PropertyInfo propInfo2 = testObj.nestedProp.GetType().GetProperty("EvenMoreNestedProp");
            PropertyInfo propInfo3 = testObj.nestedProp.EvenMoreNestedProp.GetType().GetProperty("SomeString");

            var d1 = (Func<T1, T2>) Delegate.CreateDelegate(typeof(Func<T1, T2>), propInfo1.GetMethod);
            var d2 = (Func<T2, T3>) Delegate.CreateDelegate(typeof(Func<T2, T3>), propInfo2.GetMethod);
            var d3 = (Func<T3, string>) Delegate.CreateDelegate(typeof(Func<T3, string>), propInfo3.GetMethod);

            var result = d1(testObj);
            var result2 = d2(result);
            var result3 = d3(result2);
            ;

            int repCount = 100_000_000;

            int dummy = 0;

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
                string x = f(testObj);
                if (x == null)
                    dummy++;
            }
            stopwatch.Stop();
            Console.WriteLine($"Lambda returned value: {f(testObj)}");
            Console.WriteLine($"Lambda elapsed: {stopwatch.ElapsedMilliseconds}");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount/10; i++)
            {
                string x = (string) GetPropertyValue(testObj, propertyName); //(string)propertyInfo.GetValue(testObj);
                if (x == null)
                    dummy++;
            }
            stopwatch.Stop();
            Console.WriteLine($"PropertyInfo returned value: {(string)GetPropertyValue(testObj, propertyName)}");
            Console.WriteLine($"PropertyInfo elapsed: {stopwatch.ElapsedMilliseconds}");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
                string x = d(testObj);
                if (x == null)
                    dummy++;
            }
            stopwatch.Stop();
            Console.WriteLine($"Delegate returned value: {d(testObj)}");
            Console.WriteLine($"Delegate elapsed: {stopwatch.ElapsedMilliseconds}");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
                string x = testObj.nestedProp.EvenMoreNestedProp.SomeString;
                if (x == null)
                    dummy++;
            }
            stopwatch.Stop();
            Console.WriteLine($"Getter returned value: {testObj.nestedProp.EvenMoreNestedProp.SomeString}");
            Console.WriteLine($"Getter elapsed: {stopwatch.ElapsedMilliseconds}");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
            }
            stopwatch.Stop();
            Console.WriteLine($"EmptyLoop elapsed: {stopwatch.ElapsedMilliseconds}");

            Console.ReadLine();
        }

        private static Func<TType, string> CreateFieldGetterA<TType>(string fieldOrPropName)
        {
            var param = Expression.Parameter(typeof(TType));
            Expression body = param;

            foreach (var prop in fieldOrPropName.Split("."))
            {
                body = Expression.PropertyOrField(body, prop);
            }

            Expression<Func<TType, string>> lambda = Expression.Lambda<Func<TType,string>>(body, param);

            return lambda.Compile();
        }


        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null || propName == null)
            {
                return null;
            }

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }
    }
}
