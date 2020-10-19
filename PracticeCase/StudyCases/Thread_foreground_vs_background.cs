using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Channels;

namespace PracticeCase.StudyCases
{
    class Thread_foreground_vs_background : IStudy
    {
        public void Execute()
        {

            // IN BOTH SCENARIO WORKS THE SAME

            ExecuteForegroundsThreads(); // APPLICATION IS NOT CLOSING UNTIL ALL THREADS FINISH 

            // ----- RESULT -----
            //Thread OnlyForegrounds_MainThread Id: 1 | State: Running | IsBackground: False | Priority: Normal |
            //    Thread RunAnotherThread BackgroundSetTo-FALSE   Id: 6 | State: Running | IsBackground: False | Priority: Normal |
            //    Thread BackgroundSetTo - FALSE :6 elapsed time: 0
            //Thread BackgroundSetTo-FALSE :6 elapsed time: 0,501
            //Thread BackgroundSetTo-FALSE :6 elapsed time: 1,002
            //Thread BackgroundSetTo-FALSE :6 elapsed time: 1,503
            //Exiting method ExecuteForegroundsThreads
            //    Thread BackgroundSetTo - FALSE :6 elapsed time: 2,004
            //Thread BackgroundSetTo-FALSE :6 elapsed time: 2,505
            //Thread BackgroundSetTo-FALSE :6 elapsed time: 3,006
            //Thread BackgroundSetTo-FALSE :6 elapsed time: 3,507
            //Thread BackgroundSetTo-FALSE :6 elapsed time: 4,008
            //Thread BackgroundSetTo-FALSE :6 elapsed time: 4,509

            //C:\...\ConsoleNetCore30\bin\Debug\netcoreapp3.0\ConsoleNetCore30.exe(process 68352) exited with code 0.
            //    To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
            //    Press any key to close this window. . .


            ExecuteBacgroundAndForegruondThreahds(); // APLICATION IS CLOSING BEFORE BACKGROUND TASK FINISH

            // ----- RESULT -----
            //Thread BackgroundAndForeground_MainThread Id: 1 | State: Running | IsBackground: False | Priority: Normal |
            //    Thread RunAnotherThread BackgroundSetTo-TRUE   Id: 6 | State: Background | IsBackground: True | Priority: Normal |
            //    Thread BackgroundSetTo - TRUE :6 elapsed time: 0
            //Thread BackgroundSetTo-TRUE :6 elapsed time: 0,502
            //Thread BackgroundSetTo-TRUE :6 elapsed time: 1,004
            //Thread BackgroundSetTo-TRUE :6 elapsed time: 1,505
            //Exiting method ExecuteBacgroundAndForegruondThreahds

            //C:\...\ConsoleNetCore30\bin\Debug\netcoreapp3.0\ConsoleNetCore30.exe(process 56644) exited with code 0.
            //    To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
            //    Press any key to close this window. . .


        }

        private void ExecuteBacgroundAndForegruondThreahds()
        {
            var currentThread = Thread.CurrentThread;
            DisplayInfo(currentThread, "BackgroundAndForeground_MainThread");

            var t = new Thread(new ParameterizedThreadStart(RunAnotherThread));
            t.IsBackground = true;
            t.Start("BackgroundSetTo-TRUE ");
            

            Thread.Sleep(2000);
            Console.WriteLine("Exiting method ExecuteBacgroundAndForegruondThreahds");
            //Environment.Exit(0);
            //Thread.CurrentThread.Interrupt();
        }

        private void ExecuteForegroundsThreads()
        {
            var currentThread = Thread.CurrentThread;
            DisplayInfo(currentThread, "OnlyForegrounds_MainThread");

            var t = new Thread(new ParameterizedThreadStart(RunAnotherThread));
            t.Start("BackgroundSetTo-FALSE ");


            Thread.Sleep(2000);
            Console.WriteLine("Exiting method ExecuteForegroundsThreads");
            //Environment.Exit(0);
        }

        private void RunAnotherThread(object name)
        {
            DisplayInfo(Thread.CurrentThread, $"RunAnotherThread {(string)name} ");

            var threadId = Thread.CurrentThread.ManagedThreadId;
            var interval = 5000;
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var fs = new FileStream(@"C:\Temp\Thread.txt", FileMode.OpenOrCreate))
            {
                using (var streamWriter = new StreamWriter(fs))
                {
                    do
                    {
                        var message =
                            $"Thread {name}:{threadId} elapsed time: {(double) stopWatch.ElapsedMilliseconds / 1000}";
                        streamWriter.WriteLine(message);
                        Console.WriteLine(message);

                        streamWriter.Flush();
                        Thread.Sleep(500);
                    } while (stopWatch.ElapsedMilliseconds < interval);
                }
            }
        }

        private void DisplayInfo(Thread currentThread, string name)
        {
            Console.WriteLine($"Thread {name} Id: {currentThread.ManagedThreadId} | " +
                              $"State: {currentThread.ThreadState} | " +
                              $"IsBackground: {currentThread.IsBackground} | " +
                              $"Priority: {currentThread.Priority} |");
        }
    }
}
