using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticeCase.StudyCases
{
    public class AsyncAwait_FireAndForget : IStudy
    {
        public void Execute()
        {
            Test3();
            Test4();

            Console.WriteLine("Test");
        }


        public static async Task Test()
        {
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            ContentManagement service2 = new ContentManagement();
            var contentTask = service2.GetContentAsync();
            var countTask = service2.GetCountAsync();
            var nameTask = service2.GetNameAsync();

            var content2 = await contentTask;
            var count2 = await countTask;
            var name2 = await nameTask;
            watch2.Stop();


            Console.WriteLine(watch2.ElapsedMilliseconds);
        }

        public static async Task Test2()
        {
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            ContentManagement service2 = new ContentManagement();
            var contentTask = await service2.GetContentAsync();
            var countTask = await service2.GetCountAsync();
            var nameTask = await service2.GetNameAsync();


            watch2.Stop();


            Console.WriteLine(watch2.ElapsedMilliseconds);
        }

        public static async Task Test3()
        {
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            ContentManagement service2 = new ContentManagement();
            var contentTask = service2.InitContentAsync();
            var countTask = service2.InitCountAsync();





            watch2.Stop();


            Console.WriteLine(watch2.ElapsedMilliseconds + "  Test3");
        }

        public static async Task Test4()
        {
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            ContentManagement service2 = new ContentManagement();
            await service2.InitContentAsync();
            await service2.InitCountAsync();

            watch2.Stop();


            Console.WriteLine(watch2.ElapsedMilliseconds + "  Test4");
        }


    }

    public class ContentManagement
    {
        public string GetContent()
        {
            Thread.Sleep(2000);
            return "content";
        }

        public int GetCount()
        {
            Thread.Sleep(5000);
            return 4;
        }

        public string GetName()
        {
            Thread.Sleep(3000);
            return "Matthew";
        }
        public async Task<string> GetContentAsync()
        {
            await Task.Delay(2000);
            return "content";
        }

        public async Task<int> GetCountAsync()
        {
            await Task.Delay(5000);
            return 4;
        }

        public async Task<string> GetNameAsync()
        {
            await Task.Delay(3000);
            return "Matthew";
        }

        public async Task InitCountAsync()
        {
            await Task.Delay(2000);
            Console.WriteLine("Count");
        }

        public async Task InitContentAsync()
        {
            await Task.Delay(3000);
            Console.WriteLine("Content");
        }
    }
}
