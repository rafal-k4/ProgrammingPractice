using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCase.StudyCases
{
    class ExpressionGetterPefromanceTest : IStudy
    {
        public void Execute()
        {
            var f = createFieldGetterA<TestDto>("SomeString");

            PropertyInfo propertyInfo = typeof(TestDto).GetProperty("SomeString");

            var d = (Func<TestDto, string>)Delegate.CreateDelegate(typeof(Func<TestDto, string>), propertyInfo.GetMethod);

            TestDto testDto = new TestDto()
            {
                SomeString = null,
                SomeBool = true
            };

            int repCount = 100_000_000;

            int dummy = 0;

            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
                string x = f(testDto);
                if (x == null)
                    dummy++;
            }
            stopwatch.Stop();
            Console.WriteLine($"Lambda elapsed: {stopwatch.ElapsedMilliseconds}");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
                string x = (string)propertyInfo.GetValue(testDto);
                if (x == null)
                    dummy++;
            }
            stopwatch.Stop();
            Console.WriteLine($"PropertyInfo elapsed: {stopwatch.ElapsedMilliseconds}");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
                string x = d(testDto);
                if (x == null)
                    dummy++;
            }
            stopwatch.Stop();
            Console.WriteLine($"Delegate elapsed: {stopwatch.ElapsedMilliseconds}");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
                string x = testDto.SomeString;
                if (x == null)
                    dummy++;
            }
            stopwatch.Stop();
            Console.WriteLine($"Getter elapsed: {stopwatch.ElapsedMilliseconds}");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < repCount; i++)
            {
            }
            stopwatch.Stop();
            Console.WriteLine($"EmptyLoop elapsed: {stopwatch.ElapsedMilliseconds}");

            Console.ReadLine();
        }

        private static Func<TType, string> createFieldGetterA<TType>(string fieldOrPropName)
        {
            var param = Expression.Parameter(typeof(TType));
            var get = Expression.PropertyOrField(param, fieldOrPropName);

            LambdaExpression lambda = Expression.Lambda(get, param);

            var f = (Func<TType, string>)lambda.Compile();

            return f;
        }

        //private static Func<TType, string> createFieldGetterB<TType>(string fieldOrPropName)
        //{
        //    var param = Expression.Parameter(typeof(TType));
        //    var get = Expression.PropertyOrField(param, fieldOrPropName);

        //    Expression.Lambda<Func<TType, string>>(Expression.TypeAs(Expression.Call(instanceCast, this.Property.GetGetMethod()), typeof(string)), param).Compile();

        //    return f;
        //}
    }

    class TestDto
    {
        public string SomeString { get; set; }
        public bool SomeBool { get; set; }
    }
}


