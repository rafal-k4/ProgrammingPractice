using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PracticeCase.StudyCases
{
    class Task_IsCancelled_Flag : IStudy
    {
        public void Execute()
        {


            Task t1 = TaskCancelledBeforeAwait();
            PrintInfo(t1);
            // Log:
            // Task result: Canceled, IsCompleted: True, IsCancelled: True IsCompletedSuccessfully: False, IsFaulted: False,
            // Exception: TaskCanceledException, TaskException:
            // ---- SUMMARY ----
            // When cancellation happen before Task execution, the method inside Task isn't even executed, and Exception TaskCancelledException is thrown!

            Task t2 = TaskCancelledInsideAwaitedMethod();
            PrintInfo(t2);
            // Log:
            // Method RunMethodWithCancellationToken started...
            // Task result: RanToCompletion, IsCompleted: True, IsCancelled: False IsCompletedSuccessfully: True, IsFaulted: False,
            // Exception: , TaskException:
            // ---- SUMMARY ----
            // Worst case scenario, Task was cancelled and no information was passed to the user (IsCompleted True, IsCancelled and Faulted on false and no Exception Thrown)

            Task t3 = TaskThrowBecauseOfCancellationTokenInsideMethod();
            PrintInfo(t3);
            // Log:
            // Method RunMethodWithCancellationTokenThrowIfCancelled started...
            // Task result: Canceled, IsCompleted: True, IsCancelled: True IsCompletedSuccessfully: False, IsFaulted: False,
            // Exception: OperationCanceledException, TaskException:
            // ---- SUMMARY ----
            // When cancellation happen inside executed task method, and inside is check for '.ThrowIfCancellationRequested();'
            // then Exception OperationCanceledException is thrown and IsCancelled is set true

            Task t4 = TaskWithMethodWithThrowInside();
            PrintInfo(t4);
            // Log:
            // Method RunMethodThrowInside started...
            // Task result: Faulted, IsCompleted: True, IsCancelled: False IsCompletedSuccessfully: False, IsFaulted: True,
            // Exception: Exception, TaskException: AggregateException
            // ---- SUMMARY ----
            // When exception is thrown inside method, task status is Faulted, isFaulted set to true, and AggregateException is thrown


            Console.ReadLine();
        }
        
        private void PrintInfo(Task t1)
        {
            Exception ex = null;
            try
            {
                t1.GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                ex = e;
            }

            Console.WriteLine($"Task result: {t1.Status}, IsCompleted: {t1.IsCompleted}, IsCancelled: {t1.IsCanceled} " +
                              $"IsCompletedSuccessfully: {t1.IsCompletedSuccessfully}, IsFaulted: {t1.IsFaulted}, " +
                              $"Exception: {ex?.GetType().Name}, TaskException: {t1.Exception?.GetType().Name}");
        }

        private Task TaskCancelledBeforeAwait()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Cancel();
            Task t1 = Task.Run(RunMethod, cts.Token);
            
            return t1;
        }

        private Task TaskCancelledInsideAwaitedMethod()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(200);
            Task t2 = Task.Run(() => RunMethodWithCancellationToken(cts.Token), cts.Token);
            
            return t2;
        }
        
        private Task TaskThrowBecauseOfCancellationTokenInsideMethod()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(200);
            Task t3 = Task.Run(() => RunMethodWithCancellationTokenThrowIfCancelled(cts.Token), cts.Token);
            
            return t3;
        }
        private Task TaskWithMethodWithThrowInside()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(200);
            Task t4 = Task.Run(() => RunMethodThrowInside(cts.Token), cts.Token);

            return t4;
        }

        private void RunMethod()
        {
            Console.WriteLine("Method RunMethod started ...");
            Thread.Sleep(1000);
            Console.WriteLine("Method RunMethod finished");
        }
        private void RunMethodWithCancellationToken(in CancellationToken ctsToken)
        {
            Console.WriteLine("Method RunMethodWithCancellationToken started ...");

            Thread.Sleep(1000);

            if (ctsToken.IsCancellationRequested)
                return;

            Console.WriteLine("Method RunMethodWithCancellationToken finished");
        }

        private void RunMethodWithCancellationTokenThrowIfCancelled(in CancellationToken ctsToken)
        {
            Console.WriteLine("Method RunMethodWithCancellationTokenThrowIfCancelled started ...");

            Thread.Sleep(1000);

            ctsToken.ThrowIfCancellationRequested();
            Console.WriteLine("Method RunMethodWithCancellationTokenThrowIfCancelled finished");
        }

        private void RunMethodThrowInside(in CancellationToken ctsToken)
        {
            Console.WriteLine("Method RunMethodThrowInside started ...");

            throw new Exception();
        }
    }
}
