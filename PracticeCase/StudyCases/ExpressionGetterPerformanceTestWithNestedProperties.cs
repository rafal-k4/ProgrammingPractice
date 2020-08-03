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


        public static Func<object, object> GetDelegate(object obj, string propertyName)
        {
            if (propertyName.Contains('.') == false)
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
                return (Func<object, object>) Delegate.CreateDelegate(typeof(Func<object, object>), propertyInfo.GetMethod);
            }

            var splittedProperty = propertyName.Split('.', 2);

            PropertyInfo propertyInfo1 = obj.GetType().GetProperty(splittedProperty[0]);
            //var d = (Func<object, object>)Delegate.CreateDelegate(typeof(Func<object, object>), propertyInfo1.GetMethod);

            return GetDelegate(propertyInfo1.GetValue(obj), splittedProperty[1]);

            return null;

        }

        public static PropertyInfo RecursiveExperiment(object obj, string propertyName)
        {
            if (propertyName.Contains('.') == false)
            {
                PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
                return propertyInfo;
            }

            var splittedProperty = propertyName.Split('.', 2);

            PropertyInfo propertyInfo1 = obj.GetType().GetProperty(splittedProperty[0]);

            var result = RecursiveExperiment(propertyInfo1.GetValue(obj), splittedProperty[1]);
            return result;
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

            /*
            PropertyInfo propInfo1 = testObj.GetType().GetProperty("nestedProp");
            var propValue1 = propInfo1.GetValue(testObj);
            PropertyInfo propInfo2 = propValue1.GetType().GetProperty("EvenMoreNestedProp");
            var propValue2 = propInfo2.GetValue(propValue1);
            PropertyInfo propInfo3 = propValue2.GetType().GetProperty("SomeString");
            var propValue3 = propInfo3.GetValue(propValue2);

            var d1 = (Func<T1, T2>) Delegate.CreateDelegate(typeof(Func<T1, T2>), propInfo1.GetMethod);
            var d2 = (Func<T2, T3>) Delegate.CreateDelegate(typeof(Func<T2, T3>), propInfo2.GetMethod);
            var d3 = (Func<T3, string>) Delegate.CreateDelegate(typeof(Func<T3, string>), propInfo3.GetMethod);
            */

            /*
            PropertyInfo propInfo1 = testObj.GetType().GetProperty("nestedProp");
            var d1 = (Func<T1, T2>)Delegate.CreateDelegate(typeof(Func<T1, T2>), propInfo1.GetMethod);
            var propValue1 = d1(testObj);

            PropertyInfo propInfo2 = propValue1.GetType().GetProperty("EvenMoreNestedProp");
            var d2 = (Func<T2, T3>)Delegate.CreateDelegate(typeof(Func<T2, T3>), propInfo2.GetMethod);
            var propValue2 = d2(propValue1);

            PropertyInfo propInfo3 = propValue2.GetType().GetProperty("SomeString");
            var d3 = (Func<T3, string>)Delegate.CreateDelegate(typeof(Func<T3, string>), propInfo3.GetMethod);
            var propValue3 = d3(propValue2);
            */


            PropertyInfo propInfo1 = testObj.GetType().GetProperty("nestedProp");
            var d1 = (Func<T1, T2>)Delegate.CreateDelegate(typeof(Func<T1, T2>), propInfo1.GetMethod);
            var propValue1 = d1(testObj);

            PropertyInfo propInfo2 = propValue1.GetType().GetProperty("EvenMoreNestedProp");
            var d2 = (Func<T2, T3>)Delegate.CreateDelegate(typeof(Func<T2, T3>), propInfo2.GetMethod);
            var propValue2 = d2(propValue1);

            PropertyInfo propInfo3 = propValue2.GetType().GetProperty("SomeString");
            var d3 = (Func<T3, string>)Delegate.CreateDelegate(typeof(Func<T3, string>), propInfo3.GetMethod);
            var propValue3 = d3(propValue2);

            Func<Func<Func<T1, T2>, T3>, string> monsterFunc = x => x(d1).SomeString; // it doesn't make sense



            Func<T1, Func<T2, Func<T3, string>>> monsterFunc2 = x =>
            {
                return y =>
                {
                    return z => z.SomeString;
                };
            };
            Func<T1, Func<T2, Func<T3, string>>> monsterFunc3 = x => y => z => z.SomeString;


            var resultFromMonsterFunc = monsterFunc2(testObj)(testObj.nestedProp)(testObj.nestedProp.EvenMoreNestedProp);
            var resultFromMonsterFunc3 = monsterFunc3(testObj);


            var result = d1(testObj);
            var result2 = d2(result);
            var result3 = d3(result2);

            var result4 = d3(d2(d1(testObj)));


            var result5 = RecursiveExperiment(testObj, propertyName);
            
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


            int decimatedRepCount = repCount / 10;

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < decimatedRepCount; i++)
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
                string x = d3(d2(d1(testObj)));
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
