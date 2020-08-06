using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticeCase.StudyCases
{
    class AsyncVoidVSAsyncTask : IStudy
    {
        public void Execute()
        {
            Console.WriteLine("1. Starting");
            FireAndForgetVoidWithHittedAwait();
            Console.WriteLine("1. Fire And Forget Void With Hitted Await");
            Console.WriteLine("1.-----------------------------------------");

            Console.WriteLine("2. Starting");
            FireAndForgetVoidWithoutHittedAwait(false);
            Console.WriteLine("2. Fire And Forget Void Without Hitted Await");
            Console.WriteLine("2.-----------------------------------------");

            Console.WriteLine("3. Starting");
            FireAndForgetTaskWithHittedAwait();
            Console.WriteLine("3. Fire And Forget Task With Hitted Await");
            Console.WriteLine("3.-----------------------------------------");

            Console.WriteLine("4. Starting");
            FireAndForgetTaskWithoutHittedAwait(false);
            Console.WriteLine("4. Fire And Forget Task Without Hitted Await");
            Console.WriteLine("4.-----------------------------------------");


            Console.ReadLine();
            // IN this scenario they are working the same, the result is like this:
            /*
                1. Starting
                1. Fire And Forget Void With Hitted Await
                1.-----------------------------------------
                2. Starting
                2. SYNC Delay 2. Void not hitted await
                2. Inside NOT Delayed
                2. Fire And Forget Void Without Hitted Await
                2.-----------------------------------------
                3. Starting
                3. Fire And Forget Task With Hitted Await
                3.-----------------------------------------
                4. Starting
                1. SYNC Delay 1. Void
                1. Inside Delayed
                4. SYNC Delay 4. Task not hitted await
                4. Inside NOT Delayed
                4. Fire And Forget Task Without Hitted Await
                4.-----------------------------------------
                3. SYNC Delay 3. Task
                3. Inside Delayed
             */
            // both awaits in 1 and 3 (1.void and 3.Task return type methods) are waiting inside for Task doing their job
            ;

        }

        private async void FireAndForgetVoidWithHittedAwait()
        {
            //await Task.Delay(5000);
            await SomeMethodAsync();
            IntentionallySynchronousSlowingMethod("1. Void", 1);
            Console.WriteLine("1. Inside Delayed");
        }

        private async void FireAndForgetVoidWithoutHittedAwait(bool ShouldIReachIfStatement)
        {
            if (ShouldIReachIfStatement)
            {
                //await Task.Delay(5000);
                await SomeMethodAsync();
            }

            IntentionallySynchronousSlowingMethod("2. Void not hitted await", 2);

            Console.WriteLine("2. Inside NOT Delayed");
        }



        private async Task FireAndForgetTaskWithHittedAwait()
        {
            //await Task.Delay(5000);
            await SomeMethodAsync();

            IntentionallySynchronousSlowingMethod("3. Task", 3);
            Console.WriteLine("3. Inside Delayed");
        }


        private async Task FireAndForgetTaskWithoutHittedAwait(bool ShouldIReachIfStatement)
        {
            if (ShouldIReachIfStatement)
            {
                //await Task.Delay(5000);
                await SomeMethodAsync();
            }
            IntentionallySynchronousSlowingMethod("4. Task not hitted await", 4);
            Console.WriteLine("4. Inside NOT Delayed");
        }

        private void IntentionallySynchronousSlowingMethod(string name, int methodNumber)
        {
            long count = 0;
            for (long i = 0; i < 5_000_000_000; i++)
            {
                count++;
            }
            Console.WriteLine($"{methodNumber}. SYNC Delay {name}");
        }

        private Task SomeMethodAsync()
        {
            return Task.Delay(2000);
            //return Task.Run(() => Thread.Sleep(3000));
        }
    }
}
