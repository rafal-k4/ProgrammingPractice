using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace PracticeCase.StudyCases
{
    class AsParallel_inside_LINQ : IStudy
    {
        public void Execute()
        {
            // ValidateNumber take 1s to finish
            //
            // SUMMARY -> ONLY LAST METHOD WORKS:
            //First method, no optimization, elapsed time: 4013
            //Second method, as parallel in the END, elapsed time: 4014
            //Third method, as parallel on INPUT, elapsed time: 1127

            //First method, no optimization, elapsed time: 4002
            //Second method, as parallel in the END, elapsed time: 4003
            //Third method, as parallel on INPUT, elapsed time: 1002

            //First method, no optimization, elapsed time: 4003
            //Second method, as parallel in the END, elapsed time: 4003
            //Third method, as parallel on INPUT, elapsed time: 1002

            // #1
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var result1 = (from number in numbers where ValidateNumber(number) select number).ToList();

            stopwatch.Stop();
            Console.WriteLine($"First method, no optimization, elapsed time: {stopwatch.ElapsedMilliseconds}");


            // #2
            stopwatch = new Stopwatch();
            stopwatch.Start();

            var result2 = (from number in numbers where ValidateNumber(number) select number).AsParallel().ToList();

            stopwatch.Stop();
            Console.WriteLine($"Second method, as parallel in the END, elapsed time: {stopwatch.ElapsedMilliseconds}");

            // #3
            stopwatch = new Stopwatch();
            stopwatch.Start();

            var result3 = (from number in numbers.AsParallel() where ValidateNumber(number) select number).ToList();

            stopwatch.Stop();
            Console.WriteLine($"Third method, as parallel on INPUT, elapsed time: {stopwatch.ElapsedMilliseconds}");

            Console.ReadLine();
        }

        private bool ValidateNumber(int number)
        {
            Thread.Sleep(1000);
            return true;
        }

        private IEnumerable<int> numbers
        {
            get
            {
                yield return 1;
                yield return 2;
                yield return 3;
                yield return 4;
            }
        }
    }
}
