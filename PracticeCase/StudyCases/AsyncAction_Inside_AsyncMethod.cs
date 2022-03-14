using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PracticeCase.StudyCases
{
    public class AsyncAction_Inside_AsyncMethod : IStudy
    {
        public void Execute()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Exec().GetAwaiter().GetResult();
            stopwatch.Stop();
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds}");


            stopwatch.Restart();
            Exec2().GetAwaiter().GetResult();
            stopwatch.Stop();
            Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds}");
            ;
        }

        private async Task Exec2()
        {
            await SomeAwaitableWithFuncTaskParam(async () =>
            {
                await Task.Delay(1000);
                Console.WriteLine("TEST FROM FUNC TASK");
            });
        }

        private async Task Exec()
        {
            // async await in case of Action delegate, is basically fire and forget (async void method), so this Console.Writeline("TEST") can be executed
            // only if proccess is not killed before 1 second elapsed time.

            await SomeAwaitableMethodWithDelegateParameter(async () =>
            {
                await Task.Delay(1000); 
                Console.WriteLine("TEST");
            });
        }

        private async Task SomeAwaitableWithFuncTaskParam(Func<Task> func)
        {
            await Task.Delay(1);
            await func();
        }

        private async Task SomeAwaitableMethodWithDelegateParameter(Action act)
        {
            await Task.Delay(1);
            act();
        }
    }
}
