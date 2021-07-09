using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PracticeCase.StudyCases
{
    class CollectionsBenchmark : IStudy
    {
        public void Execute()
        {
            IEnumerable<int> range = Enumerable.Range(0, 100_000_000);
            List<int> list = range.ToList();
            int[] array = range.ToArray();


            sumAllIEnumerable(range); // DEBUG: 691  RELEASE: 813
            sumAllIEnumerable(list); // DEBUG: 1134  RELEASE: 974
            sumAllIEnumerable(array); // DEBUG: 840  RELEASE: 737

            sumList(list); // DEBUG: 476  RELEASE: 275
            sumArray(array); // DEBUG: 256  RELEASE: 74

            Console.ReadLine();
        }

        private void sumArray(int[] array)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var sum = 0;
            foreach (var item in array)
            {
                sum += item;
            }
            stopwatch.Stop();

            Console.WriteLine($"{array.GetType().Name} sum: {sum}, time: {stopwatch.ElapsedMilliseconds}");
        }

        private void sumList(List<int> list)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            stopwatch.Stop();

            Console.WriteLine($"{list.GetType().Name} sum: {sum}, time: {stopwatch.ElapsedMilliseconds}");
        }

        private void sumAllIEnumerable(IEnumerable<int> list)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            stopwatch.Stop();

            Console.WriteLine($"{list.GetType().Name} sum: {sum}, time: {stopwatch.ElapsedMilliseconds}");
        }
    }
}
